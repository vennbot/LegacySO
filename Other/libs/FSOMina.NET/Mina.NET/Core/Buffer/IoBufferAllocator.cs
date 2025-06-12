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

namespace Mina.Core.Buffer
{
    /// <summary>
    /// Allocates <see cref="IoBuffer"/>s and manages them.
    /// Please implement this interface if you need more advanced memory management scheme.
    /// </summary>
    public interface IoBufferAllocator
    {
        /// <summary>
        /// Returns the buffer which is capable of the specified size.
        /// </summary>
        /// <param name="capacity">the capacity of the buffer</param>
        /// <returns>the allocated buffer</returns>
        /// <exception cref="ArgumentException">If the <paramref name="capacity"/> is a negative integer</exception>
        IoBuffer Allocate(Int32 capacity);
        /// <summary>
        /// Wraps the specified byte array into Mina.NET buffer.
        /// </summary>
        IoBuffer Wrap(Byte[] array);
        /// <summary>
        /// Wraps the specified byte array into Mina.NET buffer.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">
        /// If the preconditions on the <paramref name="offset"/> and <paramref name="length"/>
        /// parameters do not hold
        /// </exception>
        IoBuffer Wrap(Byte[] array, Int32 offset, Int32 length);
    }
}
