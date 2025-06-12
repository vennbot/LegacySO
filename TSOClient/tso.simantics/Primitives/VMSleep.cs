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
using FSO.Files.Utils;
using System.IO;

namespace FSO.SimAntics.Primitives
{
    public class VMSleep : VMPrimitiveHandler
    {
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            var operand = (VMSleepOperand)args;
            var idleStart = context.Thread.ScheduleIdleStart;

            context.Args[operand.StackVarToDec] -= (short)((idleStart != 0 && idleStart < context.VM.Scheduler.CurrentTickID) ? (context.VM.Scheduler.CurrentTickID - idleStart) : 1);

            if (context.Thread.Interrupt)
            {
                context.Thread.ScheduleIdleStart = 0;
                context.Thread.Interrupt = false;
                return VMPrimitiveExitCode.GOTO_TRUE;
            }

            if (context.Args[operand.StackVarToDec] <= -1) { 
                context.Thread.ScheduleIdleStart = 0;
                context.VM.Context.NextRandom(1); //rng cycle - for desync detect
                return (context.Caller.Dead)?VMPrimitiveExitCode.GOTO_TRUE_NEXT_TICK:VMPrimitiveExitCode.GOTO_TRUE;
            }
            else
            {
                context.Thread.ScheduleIdleStart = context.VM.Scheduler.CurrentTickID;
                context.VM.Scheduler.ScheduleTickIn(context.Caller, (uint)context.Args[operand.StackVarToDec]+1);
                return VMPrimitiveExitCode.CONTINUE_FUTURE_TICK;
            }
        }
    }

    public class VMSleepOperand : VMPrimitiveOperand
    {
        public short StackVarToDec { get; set; }

        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes){
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN)){
                StackVarToDec = io.ReadInt16();
            }
        }

        public void Write(byte[] bytes) {
            using (var io = new BinaryWriter(new MemoryStream(bytes)))
            {
                io.Write(StackVarToDec);
            }
        }
        #endregion
    }
}
