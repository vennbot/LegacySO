
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
using FSO.Server.Framework.Gluon;
using FSO.Server.Protocol.Gluon.Packets;
using FSO.Server.Servers.City.Domain;

namespace FSO.Server.Servers.City.Handlers
{
    public class MatchmakerNotifyHandler
    {
        private JobMatchmaker Matchmaker;
        private CityServerContext Context;

        public MatchmakerNotifyHandler(JobMatchmaker mm, CityServerContext context)
        {
            this.Context = context;
            this.Matchmaker = mm;
        }

        public async void Handle(IGluonSession session, MatchmakerNotify packet)
        {
            switch (packet.Mode)
            {
                case MatchmakerNotifyType.RemoveAvatar:
                    Matchmaker.RemoveAvatar(packet.LotID, packet.AvatarID);
                    break;
            }
            
        }
    }
}
