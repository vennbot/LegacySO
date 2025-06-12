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
namespace FSO.Server.Database.DA.Avatars
{
    public class DbAvatarSummary
    {
        //fso_avatar data
        public uint avatar_id { get; set; }
        public int shard_id { get; set; }
        public uint user_id { get; set; }
        public string name { get; set; }
        public DbAvatarGender gender { get; set; }
        public uint date { get; set; }
        public byte skin_tone { get; set; }
        public ulong head { get; set; }
        public ulong body { get; set; }
        public string description { get; set; }
        
        //fso_lots
        public uint? lot_id { get; set; }
        public uint? lot_location { get; set; }
        public string lot_name { get; set; }
    }
}
