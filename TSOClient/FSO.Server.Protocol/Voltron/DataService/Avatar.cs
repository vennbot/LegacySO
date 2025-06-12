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
namespace FSO.Server.Protocol.Voltron.DataService
{
    public class Avatar
    {
        public bool Avatar_IsFounder { get; set; }
        public string Avatar_Name { get; set; }
        public string Avatar_Description { get; set; }
        public bool Avatar_IsParentalControlLocked { get; set; }

        /*public bool Avatar_IsOnline { get; set; }
        public uint Avatar_LotGridXY { get; set; }
        public uint Avatar_Age { get; set; }
        public ushort Avatar_SkillsLockPoints { get; set; }

        public AvatarAppearance Avatar_Appearance { get; set; }
        public AvatarSkills Avatar_Skills { get; set; }

        public List<Bookmark> Avatar_BookmarksVec { get; set; }*/
    }
}
