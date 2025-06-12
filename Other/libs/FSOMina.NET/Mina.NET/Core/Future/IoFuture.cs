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

namespace Mina.Core.Future
{
    /// <summary>
    /// Represents the completion of an asynchronous I/O operation on an <see cref="IoSession"/>.
    /// </summary>
    public interface IoFuture
    {
        /// <summary>
        /// Gets the <see cref="IoSession"/> which is associated with this future.
        /// </summary>
        IoSession Session { get; }
        /// <summary>
        /// Returns if the asynchronous operation is completed.
        /// </summary>
        Boolean Done { get; }
        /// <summary>
        /// Event that this future is completed.
        /// If the listener is added after the completion, the listener is directly notified.
        /// </summary>
        event EventHandler<IoFutureEventArgs> Complete;
        /// <summary>
        /// Wait for the asynchronous operation to complete.
        /// </summary>
        /// <returns>self</returns>
        IoFuture Await();
        /// <summary>
        /// Wait for the asynchronous operation to complete with the specified timeout.
        /// </summary>
        /// <returns><tt>true</tt> if the operation is completed</returns>
        Boolean Await(Int32 millisecondsTimeout);
    }

    /// <summary>
    /// Contains data for events of <see cref="IoFuture"/>.
    /// </summary>
    public class IoFutureEventArgs : EventArgs
    {
        private readonly IoFuture _future;

        /// <summary>
        /// </summary>
        public IoFutureEventArgs(IoFuture future)
        {
            _future = future;
        }

        /// <summary>
        /// Gets the associated future.
        /// </summary>
        public IoFuture Future
        {
            get { return _future; }
        }
    }
}
