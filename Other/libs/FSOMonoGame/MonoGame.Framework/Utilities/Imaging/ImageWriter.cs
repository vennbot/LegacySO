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
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MonoGame.Utilities
{
    internal enum ImageWriterFormat
    {
        Bmp,
        Tga,
        Png,
        Jpg,
    }

    internal unsafe class ImageWriter
    {
        private Stream _stream;
        private byte[] _buffer = new byte[1024];

        private int WriteCallback(void* context, void* data, int size)
        {
            if (data == null || size <= 0)
            {
                return 0;
            }

            if (_buffer.Length < size)
            {
                _buffer = new byte[size*2];
            }

            var bptr = (byte*) data;

            Marshal.Copy(new IntPtr(bptr), _buffer, 0, size);

            _stream.Write(_buffer, 0, size);

            return size;
        }

        public void Write(byte[] bytes, int x, int y, int comp, ImageWriterFormat format, Stream dest)
        {
            try
            {
                _stream = dest;
                fixed (byte* b = &bytes[0])
                {
                    switch (format)
                    {
                        case ImageWriterFormat.Bmp:
                            Imaging.stbi_write_bmp_to_func(WriteCallback, null, x, y, comp, b);
                            break;
                        case ImageWriterFormat.Tga:
                            Imaging.stbi_write_tga_to_func(WriteCallback, null, x, y, comp, b);
                            break;
                        case ImageWriterFormat.Jpg:
                            Imaging.stbi_write_jpg_to_func(WriteCallback, null, x, y, comp, b, 90);
                            break;

                        case ImageWriterFormat.Png:
                            Imaging.stbi_write_png_to_func(WriteCallback, null, x, y, comp, b, x*comp);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("format", format, null);
                    }
                }
            }
            finally
            {
                _stream = null;
            }
        }
    }
}
