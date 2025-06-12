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
using FSO.SimAntics.Engine;
using FSO.SimAntics.Engine.Utils;

namespace FSO.SimAntics.Primitives
{
    //like tso transfer funds, but really scaled back (only deduct from maxis)
    public class VMTS1Budget : VMPrimitiveHandler
    {
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            var operand = (VMTransferFundsOperand)args;
            var amount = VMMemory.GetBigVariable(context, operand.GetAmountOwner(), (short)operand.AmountData);

            if ((operand.Flags & VMTransferFundsFlags.Subtract) > 0) amount = -amount; //instead of subtracting, we're adding
                                                                                       //weird terms for the flags here but ts1 is inverted
                                                                                       //amount contains the amount we are subtracting from the budget.

            var oldBudget = context.VM.TS1State.CurrentFamily?.Budget ?? 0;
            var newBudget = oldBudget - amount;
            if (oldBudget < 0) return VMPrimitiveExitCode.GOTO_FALSE;
            if ((operand.Flags & VMTransferFundsFlags.JustTest) == 0 && context.VM.TS1State.CurrentFamily != null) context.VM.TS1State.CurrentFamily.Budget = newBudget;
            return VMPrimitiveExitCode.GOTO_TRUE;

            //ts1 does have expense types, which could be used for expenses monitoring (i do not think ts1 had this)
        }
    }
}
