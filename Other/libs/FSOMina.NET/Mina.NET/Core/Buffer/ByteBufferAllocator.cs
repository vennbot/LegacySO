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
    /// A simplistic <see cref="IoBufferAllocator"/> which simply allocates a new
    /// buffer every time.
    /// </summary>
    public class ByteBufferAllocator : IoBufferAllocator
    {
        /// <summary>
        /// Static instace of <see cref="ByteBufferAllocator"/>.
        /// </summary>
        public static readonly ByteBufferAllocator Instance = new ByteBufferAllocator();

        /// <inheritdoc/>
        public IoBuffer Allocate(Int32 capacity)
        {
            if (capacity < 0)
                throw new ArgumentException("Capacity should be >= 0", "capacity");
            return new ByteBuffer(this, capacity, capacity);
        }

        /// <inheritdoc/>
        public IoBuffer Wrap(Byte[] array, Int32 offset, Int32 length)
        {
            try
            {
                return new ByteBuffer(this, array, offset, length);
            }
            catch (ArgumentException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <inheritdoc/>
        public IoBuffer Wrap(Byte[] array)
        {
            return Wrap(array, 0, array.Length);
        }
    }
}
