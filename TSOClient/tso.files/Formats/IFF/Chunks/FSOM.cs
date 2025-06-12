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
using FSO.Files.RC;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.IO.Compression;

namespace FSO.Files.Formats.IFF.Chunks
{
    /// <summary>
    /// Iff chunk wrapper for an FSOM file. 
    /// </summary>
    public class FSOM : IffChunk
    {
        private byte[] data;
        private DGRP3DMesh Cached;

        /// <summary>
        /// Reads a BMP chunk from a stream.
        /// </summary>
        /// <param name="iff">An Iff instance.</param>
        /// <param name="stream">A Stream object holding a BMP chunk.</param>
        public override void Read(IffFile iff, Stream stream)
        {
            data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
        }

        public override bool Write(IffFile iff, Stream stream)
        {
            if (data == null)
            {
                using (var cstream = new GZipStream(stream, CompressionMode.Compress))
                    Cached.Save(cstream);
            } else
            {
                stream.Write(data, 0, data.Length);
            }
            return true;
        }

        public DGRP3DMesh Get(DGRP dgrp, GraphicsDevice device)
        {
            if (Cached == null) {
                using (var stream = new MemoryStream(data)) {
                    Cached = new DGRP3DMesh(dgrp, stream, device);
                }
            }
            data = null;
            return Cached;
        }

        public void SetMesh(DGRP3DMesh mesh)
        {
            Cached = mesh;
            data = null;
        }
    }
}
