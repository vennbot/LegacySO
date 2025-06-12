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

namespace Mina.Filter.Executor
{
    /// <summary>
    /// Listens and filters all event queue operations occurring in
    /// <see cref="OrderedThreadPoolExecutor"/> and <see cref="UnorderedThreadPoolExecutor"/>.
    /// </summary>
    public interface IoEventQueueHandler
    {
        /// <summary>
        /// Returns <tt>true</tt> if and only if the specified <tt>event</tt> is
        /// allowed to be offered to the event queue.  The <tt>event</tt> is dropped
        /// if <tt>false</tt> is returned.
        /// </summary>
        Boolean Accept(Object source, IoEvent ioe);
        /// <summary>
        /// Invoked after the specified <paramref name="ioe"/> has been offered to the event queue.
        /// </summary>
        void Offered(Object source, IoEvent ioe);
        /// <summary>
        /// Invoked after the specified <paramref name="ioe"/> has been polled to the event queue.
        /// </summary>
        void Polled(Object source, IoEvent ioe);
    }

    class NoopIoEventQueueHandler : IoEventQueueHandler
    {
        public static readonly NoopIoEventQueueHandler Instance = new NoopIoEventQueueHandler();

        private NoopIoEventQueueHandler()
        { }

        public Boolean Accept(Object source, IoEvent ioe)
        {
            return true;
        }

        public void Offered(Object source, IoEvent ioe)
        {
            // NOOP
        }

        public void Polled(Object source, IoEvent ioe)
        {
            // NOOP
        }
    }
}
