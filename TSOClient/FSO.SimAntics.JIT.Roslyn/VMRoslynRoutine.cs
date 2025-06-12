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
using FSO.SimAntics.JIT.Runtime;

namespace FSO.SimAntics.JIT.Roslyn
{
    /// <summary>
    /// Compared to an AOT routine, a Roslyn routine can fall back to the interpreter if the IBHAV is not available yet.
    /// </summary>
    public class VMRoslynRoutine : VMRoutine
    {
        private delegate VMPrimitiveExitCode ExecuteDelegate(VMStackFrame frame, out VMInstruction instruction);
        private IBHAV Function;
        private IInlineBHAV IFunction;

        private ExecuteDelegate ExecuteFunction;

        public VMRoslynRoutine() : base()
        {
            ExecuteFunction = base.Execute;
        }

        public void SetJITRoutine(IBHAV bhav)
        {
            Function = bhav;
            ExecuteFunction = ExecuteJIT;
        }

        public void SetJITRoutine(IInlineBHAV bhav)
        {
            IFunction = bhav;
            ExecuteFunction = ExecuteJITInline;
        }

        public override VMPrimitiveExitCode Execute(VMStackFrame frame, out VMInstruction instruction)
        {
            return ExecuteFunction(frame, out instruction);
        }

        private VMPrimitiveExitCode ExecuteJIT(VMStackFrame frame, out VMInstruction instruction)
        {
            var result = Function.Execute(frame, ref frame.InstructionPointer);
            instruction = frame.GetCurrentInstruction();
            return result;
        }

        private VMPrimitiveExitCode ExecuteJITInline(VMStackFrame frame, out VMInstruction instruction)
        {
            var result = IFunction.Execute(frame, ref frame.InstructionPointer, frame.Args);
            instruction = frame.GetCurrentInstruction();
            return result ? VMPrimitiveExitCode.RETURN_TRUE : VMPrimitiveExitCode.RETURN_FALSE;
        }

    }
}
