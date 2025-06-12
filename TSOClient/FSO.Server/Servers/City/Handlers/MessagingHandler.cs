// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0.

/*
    Original Source: FreeSO (https://github.com/riperiperi/FreeSO)
    Original Author(s): The FreeSO Development Team

    Modifications for LegacySO by Benjamin Venn (https://github.com/vennbot):
    - Adjusted to support self-hosted LegacySO servers.
    - Modified to allow the LegacySO game client to connect to a predefined server by default.
    - Gameplay logic changes for a balanced and fair experience.
    - Updated references from "FreeSO" to "LegacySO" where appropriate.
    - Other changes documented in commit history and project README.

    Credit is retained for the original FreeSO project and its contributors.
*/
using FSO.Common.DataService;
using FSO.Server.Framework.Aries;
using FSO.Server.Framework.Voltron;
using FSO.Server.Protocol.Electron.Packets;
using System;
using System.Linq;

namespace FSO.Server.Servers.City.Handlers
{
    public class MessagingHandler
    {
        private ISessions Sessions;
        private IDataService DataService;

        public MessagingHandler(ISessions sessions, IDataService dataService)
        {
            this.Sessions = sessions;
            this.DataService = dataService;
        }

        public async void Handle(IVoltronSession session, InstantMessage message)
        {
            if (session.IsAnonymous) //CAS users can't do this.
                return;

            try
            {
                message.From = session.AvatarId;
                message.FromType = FSO.Common.Enum.UserReferenceType.AVATAR;
                message.Color |= 0xFF000000;
                var targetSession = Sessions.GetByAvatarId(message.To);
                if (targetSession == null)
                {
                    WriteFail(session, message, InstantMessageFailureReason.THEY_ARE_OFFLINE);
                    return;
                }

                var them = await DataService.Get<FSO.Common.DataService.Model.Avatar>(message.To);
                var bookmarks = them.Avatar_BookmarksVec;
                if (bookmarks.Any(x => x.Bookmark_Type == 5 && x.Bookmark_TargetID == session.AvatarId))
                {
                    WriteFail(session, message, InstantMessageFailureReason.THEY_ARE_IGNORING_YOU);
                    return;
                }

                var you = await DataService.Get<FSO.Common.DataService.Model.Avatar>(message.From);
                bookmarks = you.Avatar_BookmarksVec;
                if (bookmarks.Any(x => x.Bookmark_Type == 5 && x.Bookmark_TargetID == message.To))
                {
                    WriteFail(session, message, InstantMessageFailureReason.YOU_ARE_IGNORING_THEM);
                    return;
                }

                targetSession.Write(message);
            }
            catch (Exception e)
            {
                //unknown error
            }
        }

        public void WriteFail(IVoltronSession session, InstantMessage message, InstantMessageFailureReason fail) {
            session.Write(new InstantMessage
            {
                FromType = FSO.Common.Enum.UserReferenceType.AVATAR,
                From = message.To,
                Type = InstantMessageType.FAILURE_ACK,
                Message = "",
                AckID = message.AckID,
                Reason = fail
            });
        }
    }
}
