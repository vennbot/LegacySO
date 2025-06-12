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
using System.Collections.Immutable;

namespace FSO.Common.DataService.Model
{
    public class LotAdmitInfo : AbstractModel
    {
        private ImmutableList<uint> _LotAdmitInfo_AdmitList;
        [Persist]
        public ImmutableList<uint> LotAdmitInfo_AdmitList
        {
            get { return _LotAdmitInfo_AdmitList; }
            set
            {
                _LotAdmitInfo_AdmitList = value;
                NotifyPropertyChanged("LotAdmitInfo_AdmitList");
            }
        }

        private byte _LotAdmitInfo_AdmitMode;
        [Persist]
        public byte LotAdmitInfo_AdmitMode
        {
            get { return _LotAdmitInfo_AdmitMode; }
            set
            {
                _LotAdmitInfo_AdmitMode = value;
                NotifyPropertyChanged("LotAdmitInfo_AdmitMode");
            }
        }
        
        private ImmutableList<uint> _LotAdmitInfo_BanList;
        [Persist]
        public ImmutableList<uint> LotAdmitInfo_BanList
        {
            get { return _LotAdmitInfo_BanList; }
            set
            {
                _LotAdmitInfo_BanList = value;
                NotifyPropertyChanged("LotAdmitInfo_BanList");
            }
        }

    }
}
