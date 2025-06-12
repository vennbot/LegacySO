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
using FSO.Files.Utils;
using System.IO;

namespace FSO.Files.Formats.IFF.Chunks
{
    public class HOUS : IffChunk
    {
        public int Version;
        public int UnknownFlag;
        public int UnknownOne;
        public int UnknownNumber;
        public int UnknownNegative;
        public short CameraDir;
        public short UnknownOne2;
        public short UnknownFlag2;
        public uint GUID;
        public string RoofName;

        public override void Read(IffFile iff, Stream stream)
        {
            using (var io = IoBuffer.FromStream(stream, ByteOrder.LITTLE_ENDIAN))
            {
                var zero = io.ReadInt32();
                Version = io.ReadInt32();
                var suoh = io.ReadCString(4);
                UnknownFlag = io.ReadInt32();
                UnknownOne = io.ReadInt32();
                UnknownNumber = io.ReadInt32();
                UnknownNegative = io.ReadInt32();
                CameraDir = io.ReadInt16();
                UnknownOne2 = io.ReadInt16();
                UnknownFlag2 = io.ReadInt16();
                GUID = io.ReadUInt32();
                RoofName = io.ReadNullTerminatedString();
            }
        }
    }
}
