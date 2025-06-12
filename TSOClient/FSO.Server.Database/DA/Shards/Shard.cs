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
namespace FSO.Server.Database.DA.Shards
{
    public class Shard
    {
        public int shard_id;
        public string name;
        public int rank;
        public string map;
        public ShardStatus status;
        public string internal_host;
        public string public_host;
        public string version_name;
        public string version_number;
        public int? update_id; //new update system. set by whichever server is running the shard.
    }

    public enum ShardStatus
    {
        Up,
        Down,
        Busy,
        Full,
        Closed,
        Frontier
    }
}
