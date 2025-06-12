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
using System.Collections.Concurrent;
using System.Net;
using Mina.Core.Filterchain;
using Mina.Core.Service;
using Mina.Core.Session;

namespace Mina.Transport.Loopback
{
    /// <summary>
    /// A <see cref="IoSession"/> for loopback transport.
    /// </summary>
    class LoopbackSession : AbstractIoSession
    {
        public static readonly ITransportMetadata Metadata
            = new DefaultTransportMetadata("mina", "loopback", false, false, typeof(LoopbackEndPoint));

        private readonly LoopbackEndPoint _localEP;
        private readonly LoopbackEndPoint _remoteEP;
        private readonly LoopbackFilterChain _filterChain;
        private readonly ConcurrentQueue<Object> _receivedMessageQueue;
        private readonly LoopbackSession _remoteSession;
        private readonly Object _lock;

        /// <summary>
        /// Constructor for client-side session.
        /// </summary>
        public LoopbackSession(IoService service, LoopbackEndPoint localEP,
            IoHandler handler, LoopbackPipe remoteEntry)
            : base(service)
        {
            Config = new DefaultLoopbackSessionConfig();
            _lock = new Byte[0];
            _localEP = localEP;
            _remoteEP = remoteEntry.Endpoint;
            _filterChain = new LoopbackFilterChain(this);
            _receivedMessageQueue = new ConcurrentQueue<Object>();
            _remoteSession = new LoopbackSession(this, remoteEntry);
        }

        /// <summary>
        /// Constructor for server-side session.
        /// </summary>
        public LoopbackSession(LoopbackSession remoteSession, LoopbackPipe entry)
            : base(entry.Acceptor)
        {
            Config = new DefaultLoopbackSessionConfig();
            _lock = remoteSession._lock;
            _localEP = remoteSession._remoteEP;
            _remoteEP = remoteSession._localEP;
            _filterChain = new LoopbackFilterChain(this);
            _remoteSession = remoteSession;
            _receivedMessageQueue = new ConcurrentQueue<Object>();
        }

        public override IoProcessor Processor
        {
            get { return _filterChain.Processor; }
        }

        public override IoFilterChain FilterChain
        {
            get { return _filterChain; }
        }

        public override EndPoint LocalEndPoint
        {
            get { return _localEP; }
        }

        public override EndPoint RemoteEndPoint
        {
            get { return _remoteEP; }
        }

        public override ITransportMetadata TransportMetadata
        {
            get { return Metadata; }
        }

        public LoopbackSession RemoteSession
        {
            get { return _remoteSession; }
        }

        internal ConcurrentQueue<Object> ReceivedMessageQueue
        {
            get { return _receivedMessageQueue; }
        }

        internal Object Lock
        {
            get { return _lock; }
        }
    }
}
