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
using FSO.Server.Framework.Aries;
using FSO.Server.Framework.Voltron;
using FSO.Server.Protocol.Voltron.Packets;
using FSO.Server.Servers.Lot.Domain;

namespace FSO.Server.Servers.Lot.Handlers
{
    public class VoltronConnectionLifecycleHandler : IAriesSessionInterceptor
    {
        private LotHost Lots;
        private IDAFactory DAFactory;

        public VoltronConnectionLifecycleHandler(LotHost lots, IDAFactory da)
        {
            this.Lots = lots;
            this.DAFactory = da;
        }

        public void Handle(IVoltronSession session, ClientByePDU packet)
        {
            session.Close();
        }

        public async void SessionClosed(IAriesSession session)
        {
            if (!(session is IVoltronSession))
            {
                return;
            }

            IVoltronSession voltronSession = (IVoltronSession)session;
            Lots.SessionClosed(voltronSession);

        }

        public void SessionCreated(IAriesSession session)
        {
        }

        public void SessionMigrated(IAriesSession session)
        {
            if (!(session is IVoltronSession))
            {
                return;
            }
            IVoltronSession voltronSession = (IVoltronSession)session;
            Lots.SessionMigrated(voltronSession);
        }

        public async void SessionUpgraded(IAriesSession oldSession, IAriesSession newSession)
        {
            if (!(newSession is IVoltronSession))
            {
                return;
            }

            //Aries session has upgraded to a voltron session
            IVoltronSession voltronSession = (IVoltronSession)newSession;

            //TODO: Make sure this user is not already connected, if they are disconnect them
            newSession.Write(new HostOnlinePDU
            {
                ClientBufSize = 4096,
                HostVersion = 0x7FFF,
                HostReservedWords = 0
            });
        }
    }
}
