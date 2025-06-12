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
using Mina.Core.Buffer;

namespace Mina.Core.File
{
    /// <summary>
    /// <see cref="IFileRegion"/> based on a <see cref="FileInfo"/>.
    /// </summary>
    public class FileInfoFileRegion : IFileRegion
    {
        private readonly FileInfo _file;
        private readonly Int64 _originalPosition;
        private Int64 _position;
        private Int64 _remainingBytes;

        /// <summary>
        /// Instantiates.
        /// </summary>
        /// <param name="fileInfo">the file info</param>
        public FileInfoFileRegion(FileInfo fileInfo)
            : this(fileInfo, 0, fileInfo.Length)
        { }

        /// <summary>
        /// Instantiates.
        /// </summary>
        /// <param name="fileInfo">the file info</param>
        /// <param name="position">the start position</param>
        /// <param name="remainingBytes">the count of remaining bytes</param>
        public FileInfoFileRegion(FileInfo fileInfo, Int64 position, Int64 remainingBytes)
        {
            if (fileInfo == null)
                throw new ArgumentNullException("fileInfo");
            if (position < 0L)
                throw new ArgumentException("position may not be less than 0", "position");
            if (remainingBytes < 0L)
                throw new ArgumentException("remainingBytes may not be less than 0", "remainingBytes");

            _file = fileInfo;
            _originalPosition = position;
            _position = position;
            _remainingBytes = remainingBytes;
        }

        /// <inheritdoc/>
        public String FullName
        {
            get { return _file.FullName; }
        }

        /// <inheritdoc/>
        public Int64 Length
        {
            get { return _file.Length; }
        }

        /// <inheritdoc/>
        public Int64 Position
        {
            get { return _position; }
        }

        /// <inheritdoc/>
        public Int64 RemainingBytes
        {
            get { return _remainingBytes; }
        }

        /// <inheritdoc/>
        public Int64 WrittenBytes
        {
            get { return _position - _originalPosition; }
        }

        /// <inheritdoc/>
        public Int32 Read(IoBuffer buffer)
        {
            using (FileStream fs = _file.OpenRead())
            {
                fs.Position = _position;
                Byte[] bytes = new Byte[buffer.Remaining];
                Int32 read = fs.Read(bytes, 0, bytes.Length);
                buffer.Put(bytes, 0, read);
                Update(read);
                return read;
            }
        }

        /// <inheritdoc/>
        public void Update(Int64 amount)
        {
            _position += amount;
            _remainingBytes -= amount;
        }
    }
}
