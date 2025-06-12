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
using System.Runtime.InteropServices;

namespace MonoGame.Utilities
{
    internal abstract unsafe class Pointer : IDisposable
    {
        protected static long _allocatedTotal;
        protected static object _lock = new object();

        public abstract long Size { get; }
        public abstract void* Ptr { get; }

        public static long AllocatedTotal
        {
            get { return _allocatedTotal; }
        }

        public abstract void Dispose();

        public static implicit operator void*(Pointer ptr)
        {
            return ptr.Ptr;
        }

        public static implicit operator byte*(Pointer ptr)
        {
            return (byte*) ptr.Ptr;
        }

        public static implicit operator short*(Pointer ptr)
        {
            return (short*) ptr.Ptr;
        }
    }

    internal unsafe class PinnedArray<T> : Pointer
    {
        private GCHandle _handle;
        private bool _disposed;
        private void* _ptr;
        private long _size;

        public GCHandle Handle
        {
            get { return _handle; }
        }

        public override void* Ptr
        {
            get { return _ptr; }
        }

        public T[] Data { get; private set; }

        public T this[long index]
        {
            get { return Data[index]; }
            set { Data[index] = value; }
        }

        public long Count { get; private set; }

        public override long Size
        {
            get { return _size; }
        }

        public long ElementSize { get; private set; }

        public PinnedArray(long size)
            : this(new T[size])
        {
        }

        public PinnedArray(T[] data)
        {
            Data = data;

            _ptr = null;
            if (data != null)
            {
                _handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                var addr = _handle.AddrOfPinnedObject();
                _ptr = addr.ToPointer();
                ElementSize = Marshal.SizeOf(typeof (T));
                Count = data.Length;
                _size = ElementSize*data.Length;
            }
            else
            {
                ElementSize = 0;
                Count = 0;
                _size = 0;
            }

            lock (_lock)
            {
                _allocatedTotal += _size;
            }
        }

        ~PinnedArray()
        {
            Dispose(false);
        }

        public override void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            lock (_lock)
            {
                _allocatedTotal -= Size;
            }

            if (Data != null)
            {
                _handle.Free();
                _ptr = null;
                Data = null;
                _size = 0;
            }

            _disposed = true;
        }
    }
}
