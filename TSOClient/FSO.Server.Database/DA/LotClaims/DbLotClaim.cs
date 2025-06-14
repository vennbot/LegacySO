
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
namespace FSO.Server.Database.DA.LotClaims
{
    public class DbLotClaim
    {
        public int claim_id { get; set; }
        public int shard_id { get; set; }
        public int lot_id { get; set; }
        public string owner { get; set; }
    }

    public class DbLotStatus
    {
        public uint location { get; set; }
        public int active { get; set; }
    }

    public class DbLotActive
    {
        public int lot_id { get; set; }
        public int shard_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public uint location { get; set; }
        public uint neighborhood_id { get; set; }
        public uint admit_mode { get; set; }
        public FSO.Common.Enum.LotCategory category { get; set; }
        public int active { get; set; }
    }
}
