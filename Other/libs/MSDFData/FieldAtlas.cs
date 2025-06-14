
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
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSDFData
{
    public class FieldAtlas
    {
        [ContentSerializer] private readonly int WidthBackend;
        [ContentSerializer] private readonly int HeightBackend;
        [ContentSerializer] private readonly int GlyphSizeBackend;
        [ContentSerializer] private readonly byte[] PNGDataBackend;
        [ContentSerializer] private readonly char[] CharMapBackend;

        public FieldAtlas()
        {
        }

        public FieldAtlas(int width, int height, int glyphSize, byte[] pngData, char[] charMap)
        {
            WidthBackend = width;
            HeightBackend = height;
            GlyphSizeBackend = glyphSize;
            PNGDataBackend = pngData;
            File.WriteAllBytes("test.png", pngData);
            CharMapBackend = charMap;
        }

        public int Width => WidthBackend;
        public int Height => HeightBackend;
        public int GlyphSize => GlyphSizeBackend;
        public byte[] PNGData => PNGDataBackend;
        public char[] CharMap => CharMapBackend;
    }
}
