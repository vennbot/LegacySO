
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
namespace FSO.Server.Database.DA.Bulletin
{
    public class DbBulletinPost
    {
        public uint bulletin_id { get; set; }
        public int neighborhood_id { get; set; }
        public uint? avatar_id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public uint date { get; set; }
        public uint flags { get; set; }
        public int? lot_id { get; set; }
        public DbBulletinType type { get; set; }
        public byte deleted { get; set; }

        public string string_type { get
            {
                return type.ToString();
            }
        }
    }

    public enum DbBulletinType
    {
        mayor,
        system,
        community
    }
}
