
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
using FSO.Server.Protocol.Gluon.Model;

namespace FSO.Server.Servers.Lot.Domain
{
    public class LotContext
    {
        public uint Id;
        public int DbId;
        public int ShardId;
        public uint ClaimId;
        public ClaimAction Action;
        public bool HighMax; 

        public bool JobLot
        {
            get
            {
                return (Id & 0x40000000) > 0;
            }
        }
    }
}
