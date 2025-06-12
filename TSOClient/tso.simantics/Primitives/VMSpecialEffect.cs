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
    public class VMSpecialEffect : VMPrimitiveHandler
    {
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            //string table 305 used by this primitive for screenshot caption
            var operand = (VMSpecialEffectOperand)args;
            if (context.VM.TS1 && VM.UseWorld)
            {
                if (((byte)operand.Flags & (1<<3)) > 0) context.VM.Context.World.CenterTo(context.StackObject.WorldUI);
            }
            return VMPrimitiveExitCode.GOTO_TRUE;
        }
    }

    public class VMSpecialEffectOperand : VMPrimitiveOperand
    {
        public ushort Timeout;
        public sbyte Size;
        public sbyte Zoom;
        public sbyte Flags;
        public sbyte StrIndex;

        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes)
        {
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN)){
                Timeout = io.ReadUInt16();
                Size = (sbyte)io.ReadByte();
                Zoom = (sbyte)io.ReadByte();
                Flags = (sbyte)io.ReadByte();
                StrIndex = (sbyte)io.ReadByte();
            }
        }

        public void Write(byte[] bytes) {
            using (var io = new BinaryWriter(new MemoryStream(bytes)))
            {
                io.Write(Timeout);
                io.Write(Size);
                io.Write(Zoom);
                io.Write(Flags);
                io.Write(StrIndex);
            }
        }
        #endregion
    }
}
