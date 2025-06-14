
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
using System;

namespace FSO.Server.Database.DA.Hosts
{
    public class DbHost
    {
        public string call_sign { get; set; }
        public DbHostRole role { get; set; }
        public DbHostStatus status { get; set; }
        public string internal_host { get; set; }
        public string public_host { get; set; }
        public DateTime time_boot { get; set; }
        public int? shard_id { get; set; }
    }

    public enum DbHostRole
    {
        city,
        lot,
        task
    }

    public enum DbHostStatus
    {
        up,
        down
    }
}
