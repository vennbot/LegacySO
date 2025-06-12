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
using FSO.Server.Database.DA;
using FSO.Server.Framework.Voltron;
using FSO.Server.Protocol.Electron.Model;
using FSO.Server.Protocol.Electron.Packets;

namespace FSO.Server.Servers.City.Handlers
{
    public class ElectronFindAvatarHandler
    {
        private IDAFactory DAFactory;
        private CityServerContext Context;
        public ElectronFindAvatarHandler(IDAFactory da, CityServerContext context)
        {
            this.DAFactory = da;
            this.Context = context;
        }

        public void Handle(IVoltronSession session, FindAvatarRequest packet)
        {
            if (session.IsAnonymous) return;
            using (var da = DAFactory.Get()) {
                var privacy = da.Avatars.GetPrivacyMode(packet.AvatarId);
                if (privacy > 0)
                {
                    session.Write(new FindAvatarResponse
                    {
                        AvatarId = packet.AvatarId,
                        LotId = 0,
                        Status = FindAvatarResponseStatus.PRIVACY_ENABLED
                    });
                    return;
                }
                //TODO: get ignore status

                var claim = da.AvatarClaims.GetByAvatarID(packet.AvatarId);
                //maybe check shard id against avatar in future. The client should do this anyways, and the server providing this functionality to everyone isnt a disaster.
                var location = claim?.location ?? 0;
                session.Write(new FindAvatarResponse
                {
                    AvatarId = packet.AvatarId,
                    LotId = location,
                    Status = (location == 0)?FindAvatarResponseStatus.NOT_ON_LOT:FindAvatarResponseStatus.FOUND
                });
            }
        }
    }
}
