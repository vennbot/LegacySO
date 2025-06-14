
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
using System;
using FSO.Server.Common;
using FSO.Server.Database.DA.Tasks;

namespace FSO.Server.Servers.Tasks.Domain
{
    public class ShutdownTask : ITask
    {
        public static Action<uint, ShutdownType> ShutdownHook;
        public static TaskTuning Tuning;

        public ShutdownTask(TaskTuning tuning)
        {
            Tuning = tuning;
        }

        public void Abort()
        {
        }

        public DbTaskType GetTaskType()
        {
            return DbTaskType.shutdown;
        }

        public void Run(TaskContext context)
        {
            var sdTune = Tuning.Shutdown;
            if (sdTune == null) sdTune = new ShutdownTaskTuning();
            if (ShutdownHook != null) ShutdownHook(sdTune.warning_period, ShutdownType.RESTART);
        }
    }

    public class ShutdownTaskTuning
    {
        public uint warning_period = 60*15;
    }
}
