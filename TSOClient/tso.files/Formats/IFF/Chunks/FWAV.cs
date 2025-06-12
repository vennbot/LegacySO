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
    /// This chunk type holds the name of a sound event that this object uses.
    /// </summary>
    public class FWAV : IffChunk
    {
        public string Name;

        /// <summary>
        /// Reads a FWAV chunk from a stream.
        /// </summary>
        /// <param name="iff">An Iff instance.</param>
        /// <param name="stream">A Stream object holding a FWAV chunk.</param>
        public override void Read(IffFile iff, Stream stream)
        {
            using (var io = IoBuffer.FromStream(stream, ByteOrder.LITTLE_ENDIAN))
            {
                Name = io.ReadNullTerminatedString();
            }
        }

        public override bool Write(IffFile iff, Stream stream)
        {
            using (var io = IoWriter.FromStream(stream, ByteOrder.LITTLE_ENDIAN))
            {
                io.WriteNullTerminatedString(Name);
            }
            return true;
        }
    }
}
