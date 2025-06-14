
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
    public class Bookmark : AbstractModel
    {
        private uint _Bookmark_TargetID;

        [Key]
        public uint Bookmark_TargetID
        {
            get { return _Bookmark_TargetID; }
            set
            {
                _Bookmark_TargetID = value;
                NotifyPropertyChanged("Bookmark_TargetID");
            }
        }

        private byte _Bookmark_Type;
        public byte Bookmark_Type
        {
            get { return _Bookmark_Type; }
            set
            {
                _Bookmark_Type = value;
                NotifyPropertyChanged("Bookmark_Type");
            }
        }
    }

    public enum BookmarkType : byte
    {
        AVATAR = 0x01,
        IGNORE_AVATAR = 0x05
    }
}
