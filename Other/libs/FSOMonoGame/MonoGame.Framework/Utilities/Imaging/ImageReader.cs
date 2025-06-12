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
    internal unsafe class ImageReader
    {
        public class AnimatedGifFrame
        {
            public byte[] Data;
            public int Delay;
        }

        private Stream _stream;
        private byte[] _buffer = new byte[1024];

        private readonly Imaging.stbi_io_callbacks _callbacks;

        public ImageReader()
        {
            _callbacks = new Imaging.stbi_io_callbacks
            {
                read = ReadCallback,
                skip = SkipCallback,
                eof = Eof
            };
        }

        private int SkipCallback(void* user, int i)
        {
            return (int) _stream.Seek(i, SeekOrigin.Current);
        }

        private int Eof(void* user)
        {
            return _stream.CanRead ? 1 : 0;
        }

        private int ReadCallback(void* user, sbyte* data, int size)
        {
            if (size > _buffer.Length)
            {
                _buffer = new byte[size*2];
            }

            var res = _stream.Read(_buffer, 0, size);
            Marshal.Copy(_buffer, 0, new IntPtr(data), size);
            return res;
        }

        public byte[] Read(Stream stream, out int x, out int y, out int comp, int req_comp)
        {
            _stream = stream;

            try
            {
                int xx, yy, ccomp;
                var result = Imaging.stbi_load_from_callbacks(_callbacks, null, &xx, &yy, &ccomp, req_comp);

                x = xx;
                y = yy;
                comp = ccomp;

                if (result == null)
                {
                    throw new InvalidOperationException(Imaging.LastError);
                }

                // Convert to array
                var c = req_comp != 0 ? req_comp : comp;
                var data = new byte[x*y*c];
                Marshal.Copy(new IntPtr(result), data, 0, data.Length);
                Operations.Free(result);

                return data;
            }
            finally
            {
                _stream = null;
            }
        }
    }
}
