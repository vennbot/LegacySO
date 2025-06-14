
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
using FSO.SimAntics.Engine.Scopes;
using FSO.Files.Utils;
using FSO.SimAntics.Engine.Utils;
using System.IO;

namespace FSO.SimAntics.Primitives
{
    public class VMTestObjectType : VMPrimitiveHandler
    {
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            var operand = (VMTestObjectTypeOperand)args;
            var objectID = VMMemory.GetVariable(context, operand.IdOwner, operand.IdData);

            var obj = (operand.IdOwner == VMVariableScope.StackObjectID)?context.StackObject:context.VM.GetObjectById(objectID);
            //var obj = context.StackObject;
            if (obj == null){
                return VMPrimitiveExitCode.ERROR;
            }

            if (obj.Object.GUID == operand.GUID) return VMPrimitiveExitCode.GOTO_TRUE; //is my guid same?
            else if (obj.MasterDefinition != null && (obj.MasterDefinition.GUID == operand.GUID)) return VMPrimitiveExitCode.GOTO_TRUE; //is master guid same?
            else return VMPrimitiveExitCode.GOTO_FALSE;
        }
    }

    public class VMTestObjectTypeOperand : VMPrimitiveOperand
    {
        public uint GUID { get; set; }
        public short IdData { get; set; }
        public VMVariableScope IdOwner { get; set; }

        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes){
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN)){
                GUID = io.ReadUInt32();
                IdData = io.ReadInt16();
                IdOwner = (VMVariableScope)io.ReadByte();
            }
        }

        public void Write(byte[] bytes) {
            using (var io = new BinaryWriter(new MemoryStream(bytes)))
            {
                io.Write(GUID);
                io.Write(IdData);
                io.Write((byte)IdOwner);
            }
        }
        #endregion
    }
}
