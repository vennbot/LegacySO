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
namespace FSO.Server.Database.DA.Updates
{
    public class DbUpdateBranch
    {
        public int branch_id { get; set; }
        public string branch_name { get; set; }
        public string version_format { get; set; }
        public int last_version_number { get; set; }
        public int minor_version_number { get; set; }
        public int? current_dist_id { get; set; }
        public int? addon_id { get; set; }
        public string base_build_url { get; set; }
        public string base_server_build_url { get; set; }
        public DbUpdateBuildMode build_mode { get; set; }
        public int flags { get; set; }
    }

    public enum DbUpdateBuildMode
    {
        zip,
        teamcity
    }
}
