
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
using System.Collections.Generic;
using System.Text;

namespace FSO.SimAntics.Engine
{
    public class VMSimanticsException : Exception
    {
        private string message;
        private VMStackFrame context;
        public VMSimanticsException(string message, VMStackFrame context) : base(message)
        {
            this.context = context;
            this.message = message;
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.Append(message);
            output.AppendLine();
            output.AppendLine();
            output.Append(GetStackTrace());
            return output.ToString();
        }

        public string GetStackTrace() {
            if (context == null) return "No Stack Info.";

            var stack = context.Thread.Stack;
            return GetStackTrace(stack);
        }

        public static string GetStackTrace(List<VMStackFrame> stack)
        {
            StringBuilder output = new StringBuilder();
            string prevEE = "";
            string prevER = "";

            for (int i = stack.Count-1; i>=0; i--)
            {
                if (i == 9 && i <= stack.Count - 8) {
                    output.Append("...");
                    output.AppendLine();
                }
                if (i > 8 && i <= stack.Count - 8) continue;
                var frame = stack[i];
                //run in tree:76

                string callerStr = frame.Caller.ToString();
                string calleeStr = frame.Callee?.ToString();

                if (callerStr != prevER || calleeStr != prevEE)
                {
                    output.Append('(');
                    output.Append(callerStr);
                    output.Append(':');
                    output.Append(calleeStr);
                    output.Append(") ");
                    output.AppendLine();
                    prevEE = calleeStr;
                    prevER = callerStr;
                }

                output.Append(" > ");

                if (frame is VMRoutingFrame)
                {
                    output.Append("VMRoutingFrame with state: ");
                    output.Append(((VMRoutingFrame)frame).State.ToString());
                }
                else if (frame.Routine != null)
                {
                    output.Append(frame.Routine.Rti.Name.TrimEnd('\0'));
                    output.Append(':');
                    output.Append(frame.InstructionPointer);
                    if (frame.InstructionPointer < frame.Routine.Instructions.Length)
                    {
                        output.Append(" (");
                        var opcode = frame.GetCurrentInstruction().Opcode;
                        var primitive = (opcode > 255) ? null : VMContext.Primitives[opcode];
                        output.Append((primitive == null) ? opcode.ToString() : primitive.Name);
                        output.Append(")");
                    }
                }
                output.AppendLine();
            }

            return output.ToString();
        }
    }
}
