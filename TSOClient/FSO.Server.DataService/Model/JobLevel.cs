
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
    public class JobLevel : AbstractModel
    {
        private ushort _JobLevel_JobType;
        public ushort JobLevel_JobType
        {
            get { return _JobLevel_JobType; }
            set
            {
                _JobLevel_JobType = value;
                NotifyPropertyChanged("JobLevel_JobType");
            }
        }
        private ushort _JobLevel_JobGrade;
        public ushort JobLevel_JobGrade
        {
            get { return _JobLevel_JobGrade; }
            set
            {
                _JobLevel_JobGrade = value;
                NotifyPropertyChanged("JobLevel_JobGrade");
            }
        }
        private uint _JobLevel_JobExperience;
        public uint JobLevel_JobExperience
        {
            get { return _JobLevel_JobExperience; }
            set
            {
                _JobLevel_JobExperience = value;
                NotifyPropertyChanged("JobLevel_JobExperience");
            }
        }
    }
}
