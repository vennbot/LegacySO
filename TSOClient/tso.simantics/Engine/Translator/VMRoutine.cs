
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
using FSO.Files.Formats.IFF.Chunks;
using FSO.SimAntics.Engine;
using FSO.SimAntics.Primitives;

namespace FSO.SimAntics
{
    public class VMRoutine
    {
        public VMRoutine(){
        }

        public byte Type;
        public VMInstruction[] Instructions;
        public ushort Locals;
        public ushort Arguments;
        public ushort ID;

        /** Run time info **/
        public VMFunctionRTI Rti;

        public BHAV Chunk;
        public uint RuntimeVer;

        public virtual VMPrimitiveExitCode Execute(VMStackFrame frame, out VMInstruction instruction)
        {
            instruction = frame.GetCurrentInstruction();
            var opcode = instruction.Opcode;

            if (opcode >= 256)
            {
                frame.Thread.ExecuteSubRoutine(frame, opcode, (VMSubRoutineOperand)instruction.Operand);
                return VMPrimitiveExitCode.CONTINUE;
            }


            var primitive = VMContext.Primitives[opcode];
            if (primitive == null)
            {
                return VMPrimitiveExitCode.GOTO_TRUE;
            }

            VMPrimitiveHandler handler = primitive.GetHandler();
            return handler.Execute(frame, instruction.Operand);
        }
    }


    public class VMFunctionRTI
    {
        public string Name;
    }
}
