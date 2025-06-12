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
using FSO.Files.Formats.IFF.Chunks;
using System.IO;

namespace FSO.SimAntics.Primitives
{
    public class VMChangeActionString : VMPrimitiveHandler
    {
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            var operand = (VMChangeActionStringOperand)args;
            STR table = null;
            switch (operand.Scope)
            {
                case 0:
                    table = context.ScopeResource.Get<STR>(operand.StringTable);
                    break;
                case 1:
                    table = context.Callee.SemiGlobal.Get<STR>(operand.StringTable);
                    break;
                case 2:
                    table = context.Global.Resource.Get<STR>(operand.StringTable);
                    break;
            }

            if (table != null)
            {
                var newName = VMDialogHandler.ParseDialogString(context, table.GetString(operand.StringID - 1), table);
                if (context.Thread.IsCheck && context.Thread.ActionStrings != null) {
                    context.Thread.ActionStrings.Add(new VMPieMenuInteraction()
                    {
                        Name = newName,
                        Param0 = context.StackObjectID
                    });
                } else
                    context.Thread.ActiveAction.Name = newName;
            }
            return VMPrimitiveExitCode.GOTO_TRUE;
        }
    }

    public class VMChangeActionStringOperand : VMPrimitiveOperand
    {
        public ushort StringTable { get; set; }
        public ushort Scope { get; set; }
        public byte StringID { get; set; }

        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes)
        {
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN))
            {
                StringTable = io.ReadUInt16();
                Scope = io.ReadUInt16();
                StringID = io.ReadByte();
            }
        }

        public void Write(byte[] bytes) {
            using (var io = new BinaryWriter(new MemoryStream(bytes)))
            {
                io.Write(StringTable);
                io.Write(Scope);
                io.Write(StringID);
            }
        }
        #endregion
    }
}
