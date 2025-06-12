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
using System.Net;
using Mina.Core.Future;
using Mina.Core.Session;

namespace Mina.Core.Write
{
    public class DefaultWriteRequest : IWriteRequest
    {
        /// <summary>
        /// An empty message.
        /// </summary>
        public static readonly Byte[] EmptyMessage = new Byte[0];

        private readonly Object _message;
        private readonly IWriteFuture _future;
        private readonly EndPoint _destination;

        public DefaultWriteRequest(Object message)
            : this(message, null, null)
        { }

        public DefaultWriteRequest(Object message, IWriteFuture future)
            : this(message, future, null)
        { }

        public DefaultWriteRequest(Object message, IWriteFuture future, EndPoint destination)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            _message = message;
            _future = future ?? UnusedFuture.Instance;
            _destination = destination;
        }

        /// <inheritdoc/>
        public IWriteRequest OriginalRequest
        {
            get { return this; }
        }

        /// <inheritdoc/>
        public Object Message
        {
            get { return _message; }
        }

        /// <inheritdoc/>
        public EndPoint Destination
        {
            get { return _destination; }
        }

        /// <inheritdoc/>
        public IWriteFuture Future
        {
            get { return _future; }
        }

        /// <inheritdoc/>
        public virtual Boolean Encoded
        {
            get { return false; }
        }

        class UnusedFuture : IWriteFuture
        {
            public static readonly UnusedFuture Instance = new UnusedFuture();

            public event EventHandler<IoFutureEventArgs> Complete
            {
                add { throw new NotSupportedException(); }
                remove { throw new NotSupportedException(); }
            }

            public Boolean Written
            {
                get { return false; }
                set { }
            }

            public Exception Exception
            {
                get { return null; }
                set { }
            }

            public IoSession Session
            {
                get { return null; }
            }

            public Boolean Done
            {
                get { return true; }
            }

            public IWriteFuture Await()
            {
                return this;
            }

            public Boolean Await(Int32 timeoutMillis)
            {
                return true;
            }

            IoFuture IoFuture.Await()
            {
                return Await();
            }
        }
    }
}
