
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
using MSDFData;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;

namespace MSDFExtension
{
    public class AtlasBuilder
    {
        public int Width;
        public int Height;
        public char[] CharMap;
        private Rgba32[] RawData;
        public int GlyphSize;
        private int Progress;

        public AtlasBuilder(int totalChars, int size)
        {
            Progress = 0;
            int width = 1;
            int height = 1;
            while (width * height < totalChars)
            {
                width *= 2;
                if (width * height < totalChars)
                {
                    height *= 2;
                }
            }

            Width = width;
            Height = height;
            GlyphSize = size;
            CharMap = new char[totalChars];
            Progress = 0;

            RawData = new Rgba32[width * height * size * size];
        }

        public int AddChar(char c, Stream imageData)
        {
            var image = Image.Load(imageData);
            var buf = new Rgba32[image.Width * image.Height];
            image.SavePixelData(buf);
            return AddChar(c, buf);
        }

        public int AddChar(char c, Rgba32[] imageData)
        {
            lock (this)
            {
                CharMap[Progress] = c;
                var x = (Progress % Width) * GlyphSize;
                var y = (Progress / Width) * GlyphSize;
                var lineWidth = (Width * GlyphSize);
                var lineInd = x + y * lineWidth;
                var ind = lineInd;
                var srcInd = 0;
                for (int oy = 0; oy < GlyphSize; oy++)
                {
                    for (int ox = 0; ox < GlyphSize; ox++)
                    {
                        if (ind >= RawData.Length || ind < 0)
                        {
                            throw new Exception("dst oob: " + ind + "/" + RawData.Length);
                        }
                        if (srcInd >= imageData.Length || srcInd < 0)
                        {
                            throw new Exception("src oob: " + srcInd + "/" + imageData.Length);
                        }
                        RawData[ind++] = imageData[srcInd++];
                    }
                    lineInd += lineWidth;
                    ind = lineInd;
                }
                return Progress++;
            }
        }

        public byte[] Save()
        {
            Image<Rgba32> result = Image.LoadPixelData<Rgba32>(RawData, Width * GlyphSize, Height * GlyphSize);

            using (var str = new MemoryStream())
            {
                result.SaveAsPng(str);
                return str.ToArray();
            }
        }

        public FieldAtlas Finish()
        {
            return new FieldAtlas(Width, Height, GlyphSize, Save(), CharMap);
        }
    }
}
