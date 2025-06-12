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
using System.Collections.Concurrent;

namespace MonoGame.Utilities
{
    internal static unsafe class Operations
    {
        internal static ConcurrentDictionary<long, Pointer> _pointers = new ConcurrentDictionary<long, Pointer>();

        public static long AllocatedTotal
        {
            get { return Pointer.AllocatedTotal; }
        }

        public static void* Malloc(long size)
        {
            var result = new PinnedArray<byte>(size);
            _pointers[(long) result.Ptr] = result;

            return result.Ptr;
        }

        public static void Memcpy(void* a, void* b, long size)
        {
            var ap = (byte*) a;
            var bp = (byte*) b;
            for (long i = 0; i < size; ++i)
            {
                *ap++ = *bp++;
            }
        }

        public static void MemMove(void* a, void* b, long size)
        {
            using (var temp = new PinnedArray<byte>(size))
            {
                Memcpy(temp.Ptr, b, size);
                Memcpy(a, temp.Ptr, size);
            }
        }

        public static void Free(void* a)
        {
            Pointer pointer;
            if (!_pointers.TryRemove((long) a, out pointer))
            {
                return;
            }

            pointer.Dispose();
        }

        public static void* Realloc(void* a, long newSize)
        {
            Pointer pointer;
            if (!_pointers.TryGetValue((long) a, out pointer))
            {
                // Allocate new
                return Malloc(newSize);
            }

            if (newSize <= pointer.Size)
            {
                // Realloc not required
                return a;
            }

            var result = Malloc(newSize);
            Memcpy(result, a, pointer.Size);

            _pointers.TryRemove((long) pointer.Ptr, out pointer);
            pointer.Dispose();

            return result;
        }

        public static int Memcmp(void* a, void* b, long size)
        {
            var result = 0;
            var ap = (byte*) a;
            var bp = (byte*) b;
            for (long i = 0; i < size; ++i)
            {
                if (*ap != *bp)
                {
                    result += 1;
                }
                ap++;
                bp++;
            }

            return result;
        }
    }
}
