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
using Mina.Core.Buffer;

namespace Mina.Core.File
{
    /// <summary>
    /// Indicates the region of a file to be sent to the remote host.
    /// </summary>
    public interface IFileRegion
    {
        /// <summary>
        /// Gets the absolute filename for the underlying file.
        /// </summary>
        String FullName { get; }
        /// <summary>
        /// Gets the total length of the file.
        /// </summary>
        Int64 Length { get; }
        /// <summary>
        /// Gets the current file position from which data will be read.
        /// </summary>
        Int64 Position { get; }
        /// <summary>
        /// Gets the number of bytes remaining to be written from the file
        /// to the remote host.
        /// </summary>
        Int64 RemainingBytes { get; }
        /// <summary>
        /// Gets the total number of bytes already written.
        /// </summary>
        Int64 WrittenBytes { get; }
        /// <summary>
        /// Reads as much bytes in to a buffer as the remaining of it.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>the actual number of read bytes</returns>
        Int32 Read(IoBuffer buffer);
        /// <summary>
        /// Updates the current file position based on the specified amount.
        /// </summary>
        void Update(Int64 amount);
    }
}
