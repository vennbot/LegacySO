
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
using FSO.SimAntics.Marshals;
using FSO.SimAntics.Primitives;

namespace FSO.SimAntics.Model
{
    public class VMRuntimeHeadline
    {
        public VMSetBalloonHeadlineOperand Operand;
        public VMEntity Target;
        public VMEntity IconTarget;
        public sbyte Index;
        public int Duration;
        public int Anim;

        public VMRuntimeHeadline(VMSetBalloonHeadlineOperand op, VMEntity targ, VMEntity icon, sbyte index)
        {
            Operand = op;
            Target = targ;
            IconTarget = icon;
            Index = index;
            Duration = (op.DurationInLoops && op.Duration != -1) ? op.Duration * 15 : op.Duration;
        }

        public VMRuntimeHeadline(VMRuntimeHeadlineMarshal input, VMContext context)
        {
            Operand = input.Operand;
            Target = context.VM.GetObjectById(input.Target);
            IconTarget = context.VM.GetObjectById(input.IconTarget);
            Index = input.Index;
            Duration = input.Duration;
            Anim = input.Anim;
        }

        public VMRuntimeHeadlineMarshal Save()
        {
            var result = new VMRuntimeHeadlineMarshal();
            result.Operand = Operand;
            result.Target = (Target == null) ? (short)0 : Target.ObjectID;
            result.IconTarget = (IconTarget == null) ? (short)0 : IconTarget.ObjectID;
            result.Index = Index;
            result.Duration = Duration;
            result.Anim = Anim;
            return result;
        }
    }
}
