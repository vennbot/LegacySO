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
using System.Collections.Generic;

namespace FSO.SimAntics.Model
{
    public class VMTuningCache
    {
        public Dictionary<int, float> MotiveOverfill = new Dictionary<int, float>();

        public void UpdateTuning(VM vm)
        {
            if (vm.TS1) return;
            var table = vm.Tuning?.GetTable("overfill", vm.TSOState.PropertyCategory);
            if (table != null) MotiveOverfill = table;
        }

        public short GetLimit(VMMotive motive)
        {
            float result;
            if (MotiveOverfill.TryGetValue((int)motive, out result)) return (short)result;
            return 100;
        }
    }
}
