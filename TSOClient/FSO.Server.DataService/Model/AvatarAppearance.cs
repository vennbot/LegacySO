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

namespace FSO.Common.DataService.Model
{
    public class AvatarAppearance : AbstractModel
    {
        private ulong _AvatarAppearance_BodyOutfitID;
        public ulong AvatarAppearance_BodyOutfitID { get { return _AvatarAppearance_BodyOutfitID; } set
            {
                _AvatarAppearance_BodyOutfitID = value;
                NotifyPropertyChanged("AvatarAppearance_BodyOutfitID");
            }
        }

        private byte _AvatarAppearance_SkinTone;
        public byte AvatarAppearance_SkinTone
        {
            get { return _AvatarAppearance_SkinTone; }
            set
            {
                _AvatarAppearance_SkinTone = value;
                NotifyPropertyChanged("AvatarAppearance_SkinTone");
            }
        }

        private ulong _AvatarAppearance_HeadOutfitID;
        public ulong AvatarAppearance_HeadOutfitID
        {
            get { return _AvatarAppearance_HeadOutfitID; }
            set
            {
                _AvatarAppearance_HeadOutfitID = value;
                NotifyPropertyChanged("AvatarAppearance_HeadOutfitID");
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as AvatarAppearance;
            return (other != null
                && AvatarAppearance_BodyOutfitID == other.AvatarAppearance_BodyOutfitID
                && AvatarAppearance_SkinTone == other.AvatarAppearance_SkinTone
                && AvatarAppearance_HeadOutfitID == other.AvatarAppearance_HeadOutfitID);
        }
    }
}
