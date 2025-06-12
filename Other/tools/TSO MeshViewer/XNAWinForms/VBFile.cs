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
/*This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
If a copy of the MPL was not distributed with this file, You can obtain one at
http://mozilla.org/MPL/2.0/.

The Original Code is TSO Dressup.

The Initial Developer of the Original Code is
ddfzcsm. All Rights Reserved.

Contributor(s):
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Dressup
{
    /// <summary>
    /// Low-level filereader for reading files related to Vitaboy.
    /// </summary>
    public class VBReader : IDisposable
    {
        private Stream Stream;
        private BinaryReader Reader;

        public VBReader(Stream stream)
        {
            this.Stream = stream;
            this.Reader = new BinaryReader(stream);
        }

        public short ReadInt16()
        {
            return Endian.SwapInt16(Reader.ReadInt16());
        }

        public int ReadInt32()
        {
            return Endian.SwapInt32(Reader.ReadInt32());
        }

        public byte ReadByte()
        {
            return Reader.ReadByte();
        }

        public string ReadPascalString()
        {
            var length = ReadByte();
            return Encoding.ASCII.GetString(Reader.ReadBytes(length));
        }

        [System.Security.SecuritySafeCritical]  // auto-generated
        public virtual unsafe float ReadFloat()
        {
            var m_buffer = Reader.ReadBytes(4);
            uint tmpBuffer = (uint)(m_buffer[0] | m_buffer[1] << 8 | m_buffer[2] << 16 | m_buffer[3] << 24);

            var result = *((float*)&tmpBuffer);
            return result;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Reader.Close();
        }

        #endregion
    }
}
