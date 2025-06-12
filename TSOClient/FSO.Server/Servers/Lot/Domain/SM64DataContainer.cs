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
using Mario.Data;
using System;
using System.IO;
using System.IO.Compression;

namespace FSO.Server.Servers.Lot.Domain
{
    internal static class SM64DataContainer
    {
        private static object _lock = new object();
        private static bool _initialized;
        private static byte[] _data;

        private static void Init()
        {
            try
            {
                using (var stream = new FileStream("Content/sm64.z64", FileMode.Open, FileAccess.Read))
                {
                    var data = new RomSource(stream);

                    int animDataLength = data.AnimationEnd - data.AnimationBase;
                    stream.Seek(data.AnimationBase, SeekOrigin.Begin);
                    byte[] uncompressed = new byte[animDataLength];
                    stream.Read(uncompressed, 0, animDataLength);

                    // Compress the data.
                    using (var mem = new MemoryStream())
                    {
                        mem.Write(BitConverter.GetBytes(animDataLength), 0, 4);
                        var zipStream = new GZipStream(mem, CompressionMode.Compress);
                        zipStream.Write(uncompressed, 0, uncompressed.Length);
                        zipStream.Close();

                        _data = mem.ToArray();
                    }
                }
            }
            catch
            {
                // Don't initialize if the data can't be found.
            }
        }

        public static byte[] GetData()
        {
            lock (_lock)
            {
                if (!_initialized)
                {
                    Init();
                }

                return _data;
            }
        }
    }
}
