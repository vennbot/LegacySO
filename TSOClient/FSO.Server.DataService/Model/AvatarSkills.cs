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
using FSO.Common.DataService.Framework;
using FSO.Common.DataService.Framework.Attributes;

namespace FSO.Common.DataService.Model
{
    public class AvatarSkills : AbstractModel
    {
        [Key]
        public uint Avatar_Id { get; set; } //just used for client sync

        private ushort _AvatarSkills_Logic;
        public ushort AvatarSkills_Logic
        {
            get { return _AvatarSkills_Logic; }
            set
            {
                _AvatarSkills_Logic = value;
                NotifyPropertyChanged("AvatarSkills_Logic");
            }
        }

        private ushort _AvatarSkills_LockLv_Logic;
        public ushort AvatarSkills_LockLv_Logic
        {
            get { return _AvatarSkills_LockLv_Logic; }
            set
            {
                _AvatarSkills_LockLv_Logic = value;
                NotifyPropertyChanged("AvatarSkills_LockLv_Logic");
            }
        }

        private ushort _AvatarSkills_Body;
        public ushort AvatarSkills_Body
        {
            get { return _AvatarSkills_Body; }
            set
            {
                _AvatarSkills_Body = value;
                NotifyPropertyChanged("AvatarSkills_Body");
            }
        }

        private ushort _AvatarSkills_LockLv_Body;
        public ushort AvatarSkills_LockLv_Body
        {
            get { return _AvatarSkills_LockLv_Body; }
            set
            {
                _AvatarSkills_LockLv_Body = value;
                NotifyPropertyChanged("AvatarSkills_LockLv_Body");
            }
        }

        private ushort _AvatarSkills_LockLv_Mechanical;
        public ushort AvatarSkills_LockLv_Mechanical
        {
            get { return _AvatarSkills_LockLv_Mechanical; }
            set
            {
                _AvatarSkills_LockLv_Mechanical = value;
                NotifyPropertyChanged("AvatarSkills_LockLv_Mechanical");
            }
        }

        private ushort _AvatarSkills_LockLv_Creativity;
        public ushort AvatarSkills_LockLv_Creativity
        {
            get { return _AvatarSkills_LockLv_Creativity; }
            set
            {
                _AvatarSkills_LockLv_Creativity = value;
                NotifyPropertyChanged("AvatarSkills_LockLv_Creativity");
            }
        }

        private ushort _AvatarSkills_LockLv_Cooking;
        public ushort AvatarSkills_LockLv_Cooking
        {
            get { return _AvatarSkills_LockLv_Cooking; }
            set
            {
                _AvatarSkills_LockLv_Cooking = value;
                NotifyPropertyChanged("AvatarSkills_LockLv_Cooking");
            }
        }

        private ushort _AvatarSkills_Cooking;
        public ushort AvatarSkills_Cooking
        {
            get { return _AvatarSkills_Cooking; }
            set
            {
                _AvatarSkills_Cooking = value;
                NotifyPropertyChanged("AvatarSkills_Cooking");
            }
        }

        private ushort _AvatarSkills_Charisma;
        public ushort AvatarSkills_Charisma
        {
            get { return _AvatarSkills_Charisma; }
            set
            {
                _AvatarSkills_Charisma = value;
                NotifyPropertyChanged("AvatarSkills_Charisma");
            }
        }

        private ushort _AvatarSkills_LockLv_Charisma;
        public ushort AvatarSkills_LockLv_Charisma
        {
            get { return _AvatarSkills_LockLv_Charisma; }
            set
            {
                _AvatarSkills_LockLv_Charisma = value;
                NotifyPropertyChanged("AvatarSkills_LockLv_Charisma");
            }
        }

        private ushort _AvatarSkills_Mechanical;
        public ushort AvatarSkills_Mechanical
        {
            get { return _AvatarSkills_Mechanical; }
            set
            {
                _AvatarSkills_Mechanical = value;
                NotifyPropertyChanged("AvatarSkills_Mechanical");
            }
        }

        private ushort _AvatarSkills_Creativity;
        public ushort AvatarSkills_Creativity
        {
            get { return _AvatarSkills_Creativity; }
            set
            {
                _AvatarSkills_Creativity = value;
                NotifyPropertyChanged("AvatarSkills_Creativity");
            }
        }
    }
}
