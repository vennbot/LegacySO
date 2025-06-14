
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
    public class THMB : IffChunk
    {
        public int Width;
        public int Height;
        public int BaseYOff;
        public int XOff;
        public int AddYOff; //accounts for difference between roofed and unroofed. relative to the base.

        public override void Read(IffFile iff, Stream stream)
        {
            using (var io = IoBuffer.FromStream(stream, ByteOrder.LITTLE_ENDIAN))
            {
                Width = io.ReadInt32();
                Height = io.ReadInt32();
                BaseYOff = io.ReadInt32();
                XOff = io.ReadInt32(); //0 in all cases i've found, pretty much?
                AddYOff = io.ReadInt32();
            }
        }
    }
}
