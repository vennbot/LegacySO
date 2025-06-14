
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
    public class VMRefresh : VMPrimitiveHandler
    {
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            var operand = (VMRefreshOperand)args;
            VMEntity target = null;
            switch (operand.TargetObject)
            {
                case 0:
                    target = context.Caller;
                    break;
                case 1:
                    target = context.StackObject;
                    break;
            }

            switch (operand.RefreshType)
            {
                case 0: //graphic
                    if (target is VMGameObject)
                    {
                        var TargObj = (VMGameObject)target;
                        TargObj.RefreshGraphic();
                    }
                    break;
                case 1: //light
                    context.VM.Context.DeferredLightingRefresh.Add(context.VM.Context.GetObjectRoom(target));
                    if (target is VMGameObject) ((VMGameObject)target).RefreshLight();
                    break;
                case 2: //area contribution
                    context.VM.Context.RefreshRoomScore(context.VM.Context.GetObjectRoom(target));
                    break;
            }

            return VMPrimitiveExitCode.GOTO_TRUE;
        }
    }

    public class VMRefreshOperand : VMPrimitiveOperand
    {
        public short TargetObject { get; set; }
        public short RefreshType { get; set; }

        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes)
        {
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN)){
                TargetObject = io.ReadInt16();
                RefreshType = io.ReadInt16();
            }
        }

        public void Write(byte[] bytes) {
            using (var io = new BinaryWriter(new MemoryStream(bytes)))
            {
                io.Write(TargetObject);
                io.Write(RefreshType);
            }
        }
        #endregion
    }
}
