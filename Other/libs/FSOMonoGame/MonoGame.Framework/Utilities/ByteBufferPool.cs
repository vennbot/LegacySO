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
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Utilities
{
    internal class ByteBufferPool
    {
        public int FreeAmount
        {
            get { return _freeBuffers.Count; }
        }

        private readonly List<byte[]> _freeBuffers;

        public ByteBufferPool()
        {
            _freeBuffers = new List<byte[]>();
        }

        /// <summary>
        /// Get a buffer that is at least as big as size.
        /// </summary>
        public byte[] Get(int size)
        {
            byte[] result;
            lock (_freeBuffers)
            {
                var index = FirstLargerThan(size);

                if (index == -1)
                {
                    result = new byte[size];
                }
                else
                {
                    result = _freeBuffers[index];
                    _freeBuffers.RemoveAt(index);
                }
            }
            return result;
        }

        /// <summary>
        /// Return the given buffer to the pool.
        /// </summary>
        /// <param name="buffer"></param>
        public void Return(byte[] buffer)
        {
            lock (_freeBuffers)
            {
                var index = FirstLargerThan(buffer.Length);
                if (index == -1)
                    _freeBuffers.Add(buffer);
                else
                    _freeBuffers.Insert(index, buffer);
            }
        }

        // Find the smallest buffer that is larger than or equally large as size or -1 if none exist
        private int FirstLargerThan(int size)
        {
            if (_freeBuffers.Count == 0) return -1;

            var l = 0;
            var r = _freeBuffers.Count - 1;

            while (l <= r)
            {
                var m = (l + r)/2;
                var buffer = _freeBuffers[m];
                if (buffer.Length < size)
                {
                    l = m + 1;
                }
                else if (buffer.Length > size)
                {
                    r = m;
                    if (l == r) return l;
                }
                else
                {
                    return m;
                }
            }

            return -1;
        }
    }
}
