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
using System.Net.Sockets;
using Mina.Core.Service;

namespace Mina.Transport.Socket
{
    /// <summary>
    /// <see cref="IoConnector"/> for datagram transport (UDP/IP).
    /// </summary>
    public class AsyncDatagramConnector : AbstractSocketConnector, IDatagramConnector
    {
        /// <summary>
        /// Instantiates.
        /// </summary>
        public AsyncDatagramConnector()
            : base(new DefaultDatagramSessionConfig())
        { }

        /// <inheritdoc/>
        public new IDatagramSessionConfig SessionConfig
        {
            get { return (IDatagramSessionConfig)base.SessionConfig; }
        }

        /// <inheritdoc/>
        public override ITransportMetadata TransportMetadata
        {
            get { return AsyncDatagramSession.Metadata; }
        }

        /// <inheritdoc/>
        protected override System.Net.Sockets.Socket NewSocket(AddressFamily addressFamily)
        {
            return new System.Net.Sockets.Socket(addressFamily, SocketType.Dgram, ProtocolType.Udp);
        }

        /// <inheritdoc/>
        protected override void BeginConnect(ConnectorContext connector)
        {
            /*
             * No idea why get a SocketError.InvalidArgument in ConnectAsync.
             * Call BeginConnect instead.
             */
            connector.Socket.BeginConnect(connector.RemoteEP, ConnectCallback, connector);
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            ConnectorContext connector = (ConnectorContext)ar.AsyncState;
            try
            {
                connector.Socket.EndConnect(ar);
            }
            catch (Exception ex)
            {
                EndConnect(ex, connector);
                return;
            }

            SocketAsyncEventArgs readBuffer = new SocketAsyncEventArgs();
            readBuffer.SetBuffer(new Byte[SessionConfig.ReadBufferSize], 0, SessionConfig.ReadBufferSize);
            readBuffer.Completed += new EventHandler<SocketAsyncEventArgs>(SocketAsyncEventArgs_Completed);

            SocketAsyncEventArgs writeBuffer = new SocketAsyncEventArgs();
            writeBuffer.SetBuffer(new Byte[SessionConfig.ReadBufferSize], 0, SessionConfig.ReadBufferSize);
            writeBuffer.Completed += new EventHandler<SocketAsyncEventArgs>(SocketAsyncEventArgs_Completed);

            EndConnect(new AsyncDatagramSession(this, Processor, connector.Socket, connector.RemoteEP,
                new SocketAsyncEventArgsBuffer(readBuffer), new SocketAsyncEventArgsBuffer(writeBuffer),
                ReuseBuffer), connector);
        }

        void SocketAsyncEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.ReceiveFrom:
                    ((AsyncDatagramSession)e.UserToken).ProcessReceive(e);
                    break;
                case SocketAsyncOperation.SendTo:
                    ((AsyncDatagramSession)e.UserToken).ProcessSend(e);
                    break;
            }
        }
    }
}
