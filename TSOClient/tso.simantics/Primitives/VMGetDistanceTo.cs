
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
using FSO.SimAntics.Engine;
using FSO.Files.Utils;
using FSO.SimAntics.Engine.Utils;
using FSO.SimAntics.Engine.Scopes;
using System.IO;

namespace FSO.SimAntics.Primitives
{
    public class VMGetDistanceTo : VMPrimitiveHandler
    {
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            var operand = (VMGetDistanceToOperand)args;

            var obj1 = context.StackObject;
            VMEntity obj2;
            if ((operand.Flags & 1) == 0) obj2 = context.Caller;
            else obj2 = context.VM.GetObjectById(VMMemory.GetVariable(context, (VMVariableScope)operand.ObjectScope, operand.OScopeData));

            var pos1 = obj1.Position;
            var pos2 = obj2.Position;

            var result = (short)Math.Floor(Math.Sqrt(Math.Pow(pos1.x - pos2.x, 2) + Math.Pow(pos1.y - pos2.y, 2))/16.0);
            var levelDiff = Math.Abs(pos2.Level - pos1.Level);

            if (context.VM.TS1)
            {
                context.Thread.TempRegisters[operand.TempNum] = (short)Math.Max(20 * levelDiff, result + 5 * levelDiff);
            } 
            else
            {
                context.Thread.TempRegisters[operand.TempNum] = (short)(result + 20 * levelDiff); //LegacySO behaviour. consistently allows us to remove the level factor.
            }
                 
            return VMPrimitiveExitCode.GOTO_TRUE;
        }
    }

    public class VMGetDistanceToOperand : VMPrimitiveOperand
    { 
        public ushort TempNum { get; set; }
        public byte Flags { get; set; }
        public VMVariableScope ObjectScope { get; set; }
        public short OScopeData { get; set; }

        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes)
        {
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN))
            {
                TempNum = io.ReadUInt16();
                Flags = io.ReadByte();
                ObjectScope = (VMVariableScope)io.ReadByte();
                OScopeData = io.ReadInt16();

                if ((Flags & 1) == 0)
                {
                    ObjectScope = VMVariableScope.MyObject;
                    OScopeData = 11;
                }
                Flags |= 1;
            }
        }

        public void Write(byte[] bytes) {
            using (var io = new BinaryWriter(new MemoryStream(bytes)))
            {
                io.Write(TempNum);
                io.Write(Flags);
                io.Write((byte)ObjectScope);
                io.Write(OScopeData);
            }
        }
        #endregion
    }
}
