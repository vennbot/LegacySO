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
using FSO.Files.Utils;
using FSO.SimAntics.Engine.Utils;
using FSO.SimAntics.Engine.Scopes;
using System.IO;

namespace FSO.SimAntics.Engine.Primitives
{
    public class VMGotoRoutingSlot : VMPrimitiveHandler {
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            var operand = (VMGotoRoutingSlotOperand)args;

            if (context.Thread.IsCheck) return VMPrimitiveExitCode.GOTO_FALSE;

            var slot = VMMemory.GetSlot(context, operand.Type, operand.Data);
            if (slot == null) return VMPrimitiveExitCode.GOTO_FALSE;

            var obj = context.StackObject;
            if (obj == null) return VMPrimitiveExitCode.GOTO_FALSE;

            //Routing slots must be type 3.
            if (slot.Type == 3)
            {
                var pathFinder = context.Thread.PushNewRoutingFrame(context, !operand.NoFailureTrees);
                pathFinder.InitRoutes(slot, context.StackObject);

                return VMPrimitiveExitCode.CONTINUE;
            }
            else return VMPrimitiveExitCode.GOTO_FALSE;
        }
    }

    public class VMGotoRoutingSlotOperand : VMPrimitiveOperand
    {
        public ushort Data { get; set; }
        public VMSlotScope Type { get; set; }
        public byte Flags { get; set; }

        public bool NoFailureTrees
        {
            get
            {
                return (Flags & 1) > 0;
            }
            set
            {
                if (value) Flags |= 1;
                else Flags &= 254;
            }
        }

        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes){
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN)){
                Data = io.ReadUInt16();
                Type = (VMSlotScope)io.ReadUInt16();
                Flags = io.ReadByte();
            }
        }

        public void Write(byte[] bytes) {
            using (var io = new BinaryWriter(new MemoryStream(bytes)))
            {
                io.Write(Data);
                io.Write((ushort)Type);
                io.Write(Flags);
            }
        }
        #endregion

        public override string ToString()
        {
            return "Go To Routing Slot (" + Type.ToString() + ": #" + Data + ")";
        }
    }

}
