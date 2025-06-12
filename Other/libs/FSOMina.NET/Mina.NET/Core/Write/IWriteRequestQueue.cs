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
using Mina.Core.Session;

namespace Mina.Core.Write
{
    /// <summary>
    /// Stores <see cref="IWriteRequest"/>s which are queued to an <see cref="IoSession"/>.
    /// </summary>
    public interface IWriteRequestQueue
    {
        /// <summary>
        /// Gets the first request available in the queue for a session.
        /// </summary>
        /// <param name="session">the associated session</param>
        /// <returns>the first available request, if any.</returns>
        IWriteRequest Poll(IoSession session);
        /// <summary>
        /// Adds a new WriteRequest to the session write's queue
        /// </summary>
        /// <param name="session">the associated session</param>
        /// <param name="writeRequest">the writeRequest to add</param>
        void Offer(IoSession session, IWriteRequest writeRequest);
        /// <summary>
        /// Tells if the WriteRequest queue is empty or not for a session.
        /// </summary>
        /// <param name="session">the associated session</param>
        /// <returns><code>true</code> if the writeRequest queue is empty</returns>
        Boolean IsEmpty(IoSession session);
        /// <summary>
        /// Removes all the requests from this session's queue.
        /// </summary>
        /// <param name="session">the associated session</param>
        void Clear(IoSession session);
        /// <summary>
        /// Disposes any releases associated with the specified session.
        /// This method is invoked on disconnection.
        /// </summary>
        /// <param name="session">the associated session</param>
        void Dispose(IoSession session);
        /// <summary>
        /// Gets number of objects currently stored in the queue.
        /// </summary>
        Int32 Size { get; }
    }
}
