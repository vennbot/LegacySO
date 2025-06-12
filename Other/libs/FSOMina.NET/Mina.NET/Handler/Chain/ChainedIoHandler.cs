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
using Mina.Core.Service;
using Mina.Core.Session;

namespace Mina.Handler.Chain
{
    /// <summary>
    /// An <see cref="IoHandler"/> which executes an <see cref="IoHandlerChain"/>
    /// on a <tt>messageReceived</tt> event.
    /// </summary>
    public class ChainedIoHandler : IoHandlerAdapter
    {
        private readonly IoHandlerChain _chain;

        /// <summary>
        /// </summary>
        public ChainedIoHandler()
            : this(new IoHandlerChain())
        { }

        /// <summary>
        /// </summary>
        public ChainedIoHandler(IoHandlerChain chain)
        {
            if (chain == null)
                throw new ArgumentNullException("chain");
            _chain = chain;
        }

        /// <summary>
        /// Gets the associated <see cref="IoHandlerChain"/>.
        /// </summary>
        public IoHandlerChain Chain
        {
            get { return _chain; }
        }

        /// <inheritdoc/>
        public override void MessageReceived(IoSession session, Object message)
        {
            _chain.Execute(null, session, message);
        }
    }
}
