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
    public class ElectionCycle : AbstractModel
    {
        private uint _ElectionCycle_StartDate;
        public uint ElectionCycle_StartDate
        {
            get { return _ElectionCycle_StartDate; }
            set
            {
                _ElectionCycle_StartDate = value;
                NotifyPropertyChanged("ElectionCycle_StartDate");
            }
        }

        private uint _ElectionCycle_EndDate;
        public uint ElectionCycle_EndDate
        {
            get { return _ElectionCycle_EndDate; }
            set
            {
                _ElectionCycle_EndDate = value;
                NotifyPropertyChanged("ElectionCycle_EndDate");
            }
        }

        private byte _ElectionCycle_CurrentState;
        public byte ElectionCycle_CurrentState
        {
            get { return _ElectionCycle_CurrentState; }
            set
            {
                _ElectionCycle_CurrentState = value;
                NotifyPropertyChanged("ElectionCycle_CurrentState");
            }
        }

        private byte _ElectionCycle_ElectionType;
        public byte ElectionCycle_ElectionType
        {
            get { return _ElectionCycle_ElectionType; }
            set
            {
                _ElectionCycle_ElectionType = value;
                NotifyPropertyChanged("ElectionCycle_ElectionType");
            }
        }
    }
}
