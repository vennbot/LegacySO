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
using System.IO;
using FSO.Files.Utils;

namespace FSO.Files.Formats.IFF.Chunks
{
    /// <summary>
    /// This chunk type holds a number of constants that behavior code can refer to. 
    /// Labels may be provided for them in a TRCN chunk with the same ID.
    /// </summary>
    public class BCON : IffChunk
    {
        public byte Flags;
        public ushort[] Constants = new ushort[0];

        /// <summary>
        /// Reads a BCON chunk from a stream.
        /// </summary>
        /// <param name="iff">An Iff instance.</param>
        /// <param name="stream">A Stream instance holding a BCON.</param>
        public override void Read(IffFile iff, Stream stream)
        {
            using (var io = IoBuffer.FromStream(stream, ByteOrder.LITTLE_ENDIAN))
            {
                var num = io.ReadByte();
                Flags = io.ReadByte();

                Constants = new ushort[num];
                for (var i = 0; i < num; i++)
                {
                    Constants[i] = io.ReadUInt16();
                }
            }
        }

        public override bool Write(IffFile iff, Stream stream)
        {
            using (var io = IoWriter.FromStream(stream, ByteOrder.LITTLE_ENDIAN))
            {
                io.WriteByte((byte)Constants.Length);
                io.WriteByte(Flags);
                foreach (var c in Constants)
                {
                    io.WriteUInt16(c);
                }

                return true;
            }
        }
    }
}
