
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

namespace FSO.Server.Database.DA.Updates
{
    public class DbUpdate
    {
        public int update_id { get; set; }
        public string version_name { get; set; }
        public int? addon_id { get; set; }
        public int branch_id { get; set; }
        public string full_zip { get; set; }
        public string incremental_zip { get; set; }
        public string manifest_url { get; set; }
        public string server_zip { get; set; }
        public int? last_update_id { get; set; }
        public int flags { get; set; }
        public DateTime date { get; set; }
        public DateTime? publish_date { get; set; }
        public DateTime? deploy_after { get; set; }
    }
}
