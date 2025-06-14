
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
    //See VMFindBestObjectForFunction for function map table.

    public class VMRunFunctionalTree : VMPrimitiveHandler
    {
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            var operand = (VMRunFunctionalTreeOperand)args;
            if (operand.Function == 65535) return VMPrimitiveExitCode.GOTO_FALSE; //wedding cake: invalid primitive operand

            var entry = VMFindBestObjectForFunction.FunctionToEntryPoint[operand.Function];
            var ent = context.StackObject;
            if (ent == null || ent.Dead) return VMPrimitiveExitCode.GOTO_FALSE;
            if (ent.EntryPoints[entry].ActionFunction != 0)
            {
                bool Execute;
                if (ent.EntryPoints[entry].ConditionFunction != 0) //check if we can definitely execute this...
                {
                    var Behavior = ent.GetRoutineWithOwner(ent.EntryPoints[entry].ConditionFunction, context.VM.Context);
                    if (Behavior != null)
                    {
                        Execute = (VMThread.EvaluateCheck(context.VM.Context, context.Caller, new VMStackFrame()
                        {
                            Caller = context.Caller,
                            Callee = ent,
                            CodeOwner = Behavior.owner,
                            StackObject = ent,
                            Routine = Behavior.routine,
                            Args = new short[4]
                        }) == VMPrimitiveExitCode.RETURN_TRUE);
                    }
                    else Execute = true;
                }
                else
                {
                    Execute = true;
                }

                if (Execute)
                {
                    //push it onto our stack, except now the stack object owns our soul!
                    var tree = ent.GetRoutineWithOwner(ent.EntryPoints[entry].ActionFunction, context.VM.Context);
                    if (tree == null) return VMPrimitiveExitCode.GOTO_FALSE; //does not exist
                    var routine = tree.routine;
                    var childFrame = new VMStackFrame
                    {
                        Routine = routine,
                        Caller = context.Caller,
                        Callee = ent,
                        CodeOwner = tree.owner,
                        StackObject = ent,
                        ActionTree = context.ActionTree
                    };
                    if (operand.Flags > 0 && context.ActionTree) context.Thread.ActiveAction.IconOwner = context.StackObject;
                    childFrame.Args = new short[routine.Arguments];
                    context.Thread.Push(childFrame);
                    return VMPrimitiveExitCode.CONTINUE;
                }
                else
                {
                    return VMPrimitiveExitCode.GOTO_FALSE;
                }
            }
            else
            {
                return VMPrimitiveExitCode.GOTO_FALSE;
            }
        }
    }

    public class VMRunFunctionalTreeOperand : VMPrimitiveOperand
    {
        public ushort Function { get; set; }
        public byte Flags { get; set; } //only flag is 1: change icon

        public bool ChangeIcon { get { return (Flags & 1) > 0; } set { Flags = (byte)((Flags & 0xFE) | (ChangeIcon ? 1 : 0)); } }

        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes)
        {
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN))
            {
                Function = io.ReadUInt16();
                Flags = io.ReadByte();
            }
        }

        public void Write(byte[] bytes) {
            using (var io = new BinaryWriter(new MemoryStream(bytes)))
            {
                io.Write(Function);
                io.Write(Flags);
            }
        }
        #endregion
    }

}
