
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
namespace FSO.Server.Protocol.Voltron.DataService
{
    public class AvatarSkills
    {
        public ushort AvatarSkills_Logic { get; set; }
        public ushort AvatarSkills_LockLv_Logic { get; set; }
        public ushort AvatarSkills_Body { get; set; }
        public ushort AvatarSkills_LockLv_Body { get; set; }
        public ushort AvatarSkills_LockLv_Mechanical { get; set; }
        public ushort AvatarSkills_LockLv_Creativity { get; set; }
        public ushort AvatarSkills_LockLv_Cooking { get; set; }
        public ushort AvatarSkills_Cooking { get; set; }
        public ushort AvatarSkills_Charisma { get; set; }
        public ushort AvatarSkills_LockLv_Charisma { get; set; }
        public ushort AvatarSkills_Mechanical { get; set; }
        public ushort AvatarSkills_Creativity { get; set; }
    }
}
