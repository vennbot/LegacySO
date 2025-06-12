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
    /// Default implementation of <see cref="IExceptionHandler"/>.
    /// </summary>
    public class ExceptionHandler<E> : IExceptionHandler<E> where E : Exception
    {
        public static readonly IExceptionHandler<Exception> Noop = new NoopExceptionHandler();
        public static readonly IExceptionHandler<Exception> Close = new CloseExceptionHandler();

        private readonly Action<IoSession, E> _act;

        /// <summary>
        /// </summary>
        public ExceptionHandler()
        { }

        /// <summary>
        /// </summary>
        public ExceptionHandler(Action<IoSession, E> act)
        {
            if (act == null)
                throw new ArgumentNullException("act");
            _act = act;
        }

        /// <inheritdoc/>
        public virtual void ExceptionCaught(IoSession session, E cause)
        {
            if (_act != null)
                _act(session, cause);
        }

        void IExceptionHandler.ExceptionCaught(IoSession session, Exception cause)
        {
            ExceptionCaught(session, (E)cause);
        }
    }

    class NoopExceptionHandler : IExceptionHandler<Exception>
    {
        internal NoopExceptionHandler() { }

        public void ExceptionCaught(IoSession session, Exception cause)
        {
            // Do nothing
        }
    }

    class CloseExceptionHandler : IExceptionHandler<Exception>
    {
        internal CloseExceptionHandler() { }

        public void ExceptionCaught(IoSession session, Exception cause)
        {
            session.Close(true);
        }
    }
}
