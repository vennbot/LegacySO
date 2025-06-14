
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
using System.Collections.Immutable;

namespace FSO.Common.DataService.Model
{
    public class Top100ListFilter : AbstractModel
    {
        private uint _Top100ListFilter_Top100ListID;
        public uint Top100ListFilter_Top100ListID
        {
            get { return _Top100ListFilter_Top100ListID; }
            set
            {
                _Top100ListFilter_Top100ListID = value;
                NotifyPropertyChanged("Top100ListFilter_Top100ListID");
            }
        }

        public ImmutableList<uint> _Top100ListFilter_ResultsVec;
        public ImmutableList<uint> Top100ListFilter_ResultsVec
        {
            get { return _Top100ListFilter_ResultsVec; }
            set
            {
                _Top100ListFilter_ResultsVec = value;
                NotifyPropertyChanged("Top100ListFilter_ResultsVec");
            }
        }
    }
}
