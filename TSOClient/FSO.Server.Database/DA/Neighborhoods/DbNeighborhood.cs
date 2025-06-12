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
namespace FSO.Server.Database.DA.Neighborhoods
{
    public class DbNeighborhood
    {
        public int neighborhood_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int shard_id { get; set; }

        public uint location { get; set; }
        public uint color { get; set; }
        public uint flag { get; set; }

        public int? town_hall_id { get; set; }
        public string icon_url { get; set; }
        public string guid { get; set; }

        public uint? mayor_id { get; set; }
        public uint mayor_elected_date { get; set; }
        public uint? election_cycle_id { get; set; }
    }
}
