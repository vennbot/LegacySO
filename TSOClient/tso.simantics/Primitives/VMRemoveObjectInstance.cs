
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
using System.IO;

namespace FSO.SimAntics.Engine.Primitives
{
    public class VMRemoveObjectInstance : VMPrimitiveHandler
    {
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            var operand = (VMRemoveObjectInstanceOperand)args;
            VMEntity obj;
            if (operand.Target == 0) obj = context.Caller;
            else obj = context.StackObject;

            //TODO: what do CleanupAll and ReturnImmediately do?
            //cleanup all likely resets all avatars using the object, though that should really happen regardless?
            //return immediately is an actual mystery
            obj?.Delete(true, context.VM.Context);

            //if (obj == context.StackObject) context.StackObject = null;

            // yield if we are going to delete
            return (obj == context.Caller) ? VMPrimitiveExitCode.GOTO_TRUE_NEXT_TICK : VMPrimitiveExitCode.GOTO_TRUE;
        }
    }

    public class VMRemoveObjectInstanceOperand : VMPrimitiveOperand
    {
        public short Target { get; set; }
        public byte Flags { get; set; }

        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes)
        {
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN))
            {
                Target = io.ReadInt16();
                Flags = io.ReadByte();
            }
        }

        public void Write(byte[] bytes) {
            using (var io = new BinaryWriter(new MemoryStream(bytes)))
            {
                io.Write(Target);
                io.Write(Flags);
            }
        }
        #endregion

        public bool ReturnImmediately
        {
            get
            {
                return ((Flags & 1) == 1);
            }
            set
            {
                if (value) Flags |= 1;
                else Flags &= unchecked((byte)~1);
            }
        }

        public bool CleanupAll
        {
            get
            {
                return ((Flags & 2) == 2);
            }
            set
            {
                if (value) Flags |= 2;
                else Flags &= unchecked((byte)~2);
            }
        }
    }
}
