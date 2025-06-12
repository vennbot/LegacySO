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

namespace FSO.SimAntics.Primitives
{
    // Logs a string to the console. Might make this functional again when we implement object development.

    public class VMSysLog : VMPrimitiveHandler
    {
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            return VMPrimitiveExitCode.GOTO_TRUE;
        }
    }

    public class VMSysLogOperand : VMPrimitiveOperand
    {
        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes)
        {
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN))
            {
            }
        }

        public void Write(byte[] bytes) { }
        #endregion
    }
}
