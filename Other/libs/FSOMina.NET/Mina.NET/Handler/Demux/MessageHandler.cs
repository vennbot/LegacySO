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

namespace Mina.Handler.Demux
{
    /// <summary>
    /// Default implementation of <see cref="IMessageHandler"/>.
    /// </summary>
    public class MessageHandler<T> : IMessageHandler<T>
    {
        public static readonly IMessageHandler<Object> Noop = new NoopMessageHandler();

        private readonly Action<IoSession, T> _act;

        /// <summary>
        /// </summary>
        public MessageHandler()
        { }

        /// <summary>
        /// </summary>
        public MessageHandler(Action<IoSession, T> act)
        {
            if (act == null)
                throw new ArgumentNullException("act");
            _act = act;
        }

        /// <inheritdoc/>
        public virtual void HandleMessage(IoSession session, T message)
        {
            if (_act != null)
                _act(session, message);
        }

        void IMessageHandler.HandleMessage(IoSession session, Object message)
        {
            HandleMessage(session, (T)message);
        }
    }

    class NoopMessageHandler : IMessageHandler<Object>
    {
        internal NoopMessageHandler() { }

        public void HandleMessage(IoSession session, Object message)
        {
            // Do nothing
        }
    }
}
