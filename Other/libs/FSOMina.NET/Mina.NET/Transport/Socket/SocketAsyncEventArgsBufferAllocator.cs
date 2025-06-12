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

namespace Mina.Transport.Socket
{
    /// <summary>
    /// An <see cref="IoBufferAllocator"/> which allocates <see cref="SocketAsyncEventArgsBuffer"/>.
    /// </summary>
    public class SocketAsyncEventArgsBufferAllocator : IoBufferAllocator
    {
        /// <summary>
        /// Static instance.
        /// </summary>
        public static readonly SocketAsyncEventArgsBufferAllocator Instance = new SocketAsyncEventArgsBufferAllocator();

        /// <summary>
        /// Returns the buffer which is capable of the specified size.
        /// </summary>
        /// <param name="capacity">the capacity of the buffer</param>
        /// <returns>the allocated buffer</returns>
        /// <exception cref="ArgumentException">If the <paramref name="capacity"/> is a negative integer</exception>
        public SocketAsyncEventArgsBuffer Allocate(Int32 capacity)
        {
            if (capacity < 0)
                throw new ArgumentException("Capacity should be >= 0", "capacity");
            return new SocketAsyncEventArgsBuffer(this, capacity, capacity);
        }

        /// <summary>
        /// Wraps the specified byte array into a <see cref="SocketAsyncEventArgsBuffer"/>.
        /// </summary>
        public SocketAsyncEventArgsBuffer Wrap(Byte[] array)
        {
            return Wrap(array, 0, array.Length);
        }

        /// <summary>
        /// Wraps the specified byte array into a <see cref="SocketAsyncEventArgsBuffer"/>.
        /// </summary>
        public SocketAsyncEventArgsBuffer Wrap(Byte[] array, Int32 offset, Int32 length)
        {
            try
            {
                return new SocketAsyncEventArgsBuffer(this, array, offset, length);
            }
            catch (ArgumentException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        IoBuffer IoBufferAllocator.Allocate(Int32 capacity)
        {
            return Allocate(capacity);
        }

        IoBuffer IoBufferAllocator.Wrap(Byte[] array)
        {
            return Wrap(array);
        }

        IoBuffer IoBufferAllocator.Wrap(Byte[] array, Int32 offset, Int32 length)
        {
            return Wrap(array, offset, length);
        }
    }
}
