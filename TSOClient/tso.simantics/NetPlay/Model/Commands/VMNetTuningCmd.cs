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
using FSO.Common.Model;
using System.Collections.Generic;
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetTuningCmd : VMNetCommandBodyAbstract
    {
        public DynamicTuning Tuning;

        public override bool Execute(VM vm)
        {
            vm.Tuning = Tuning;
            vm.UpdateTuning();
            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            return !FromNet;
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            Tuning = new DynamicTuning(reader);
        }

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            if (Tuning == null) Tuning = new DynamicTuning(new List<DynTuningEntry>());
            Tuning.SerializeInto(writer);
        }
    }
}
