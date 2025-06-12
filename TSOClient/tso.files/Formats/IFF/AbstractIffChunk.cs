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

namespace FSO.Files.Formats.IFF
{
    /// <summary>
    /// An IFF is made up of chunks.
    /// </summary>
    public abstract class IffChunk 
    {
        public ushort ChunkID;
        public ushort ChunkFlags;
        public string ChunkType; //just making it easier to access
        public string ChunkLabel;
        public bool ChunkProcessed;
        public byte[] OriginalData; //IDE ONLY: always contains base data for any chunk.
        public ushort OriginalID;
        public bool AddedByPatch;
        public string OriginalLabel;
        public byte[] ChunkData;
        public IffFile ChunkParent;

        public ChunkRuntimeState RuntimeInfo = ChunkRuntimeState.Normal;

        /// <summary>
        /// Reads this chunk from an IFF.
        /// </summary>
        /// <param name="iff">The IFF to read from.</param>
        /// <param name="stream">The stream to read from.</param>
        public abstract void Read(IffFile iff, Stream stream);

        /// <summary>
        /// The name of this chunk.
        /// </summary>
        /// <returns>The name of this chunk as a string.</returns>
        public override string ToString()
        {
            return "#" + ChunkID.ToString() + " " + ChunkLabel;
        }

        /// <summary>
        /// Attempts to write this chunk to a stream (presumably an IFF or PIFF)
        /// </summary>
        /// <param name="iff"></param>
        /// <param name="stream"></param>
        /// <returns>True if data has been written, false if not. </returns>
        public virtual bool Write(IffFile iff, Stream stream) { return false; }

        public virtual void Dispose() { }

    }
}
