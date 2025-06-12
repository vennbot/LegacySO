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
using System.Collections.Generic;

namespace Mina.Util
{
    /// <summary>
    /// Represents a FIFO queue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueue<T> : IEnumerable<T>
    {
        /// <summary>
        /// Checks if this queue is empty.
        /// </summary>
        Boolean IsEmpty { get; }
        /// <summary>
        /// Enqueue an item.
        /// </summary>
        void Enqueue(T item);
        /// <summary>
        /// Dequeue an item.
        /// </summary>
        T Dequeue();
        /// <summary>
        /// Gets the count of items in this queue.
        /// </summary>
        Int32 Count { get; }
    }

    class Queue<T> : System.Collections.Generic.Queue<T>, IQueue<T>
    {
        public Boolean IsEmpty
        {
            get { return base.Count == 0; }
        }

        T IQueue<T>.Dequeue()
        {
            return IsEmpty ? default(T) : base.Dequeue();
        }
    }

    class ConcurrentQueue<T> : System.Collections.Concurrent.ConcurrentQueue<T>, IQueue<T>
    {
        public T Dequeue()
        {
            T e = default(T);
            this.TryDequeue(out e);
            return e;
        }
    }
}
