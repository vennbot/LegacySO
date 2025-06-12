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
    /// A <see cref="IoEventExecutor"/> that does not maintain the order of <see cref="IoEvent"/>s.
    /// This means more than one event handler methods can be invoked at the same time with mixed order.
    /// If you need to maintain the order of events per session, please use
    /// <see cref="OrderedThreadPoolExecutor"/>.
    /// </summary>
    public class UnorderedThreadPoolExecutor : ThreadPoolExecutor, IoEventExecutor
    {
        private readonly IoEventQueueHandler _queueHandler;

        /// <summary>
        /// Instantiates with a <see cref="NoopIoEventQueueHandler"/>.
        /// </summary>
        public UnorderedThreadPoolExecutor()
            : this(null)
        { }

        /// <summary>
        /// Instantiates with the given <see cref="IoEventQueueHandler"/>.
        /// </summary>
        /// <param name="queueHandler">the handler</param>
        public UnorderedThreadPoolExecutor(IoEventQueueHandler queueHandler)
        {
            _queueHandler = queueHandler == null ? NoopIoEventQueueHandler.Instance : queueHandler;
        }

        /// <summary>
        /// Gets the <see cref="IoEventQueueHandler"/>.
        /// </summary>
        public IoEventQueueHandler QueueHandler
        {
            get { return _queueHandler; }
        }

        /// <inheritdoc/>
        public void Execute(IoEvent ioe)
        {
            Boolean offeredEvent = _queueHandler.Accept(this, ioe);
            if (offeredEvent)
            {
                Execute(() =>
                {
                    _queueHandler.Polled(this, ioe);
                    ioe.Fire();
                });

                _queueHandler.Offered(this, ioe);
            }
        }
    }
}
