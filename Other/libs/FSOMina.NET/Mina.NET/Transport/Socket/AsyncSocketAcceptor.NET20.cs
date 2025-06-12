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
using Mina.Util;

namespace Mina.Transport.Socket
{
    public class AsyncSocketAcceptor : AbstractSocketAcceptor
    {
        public AsyncSocketAcceptor()
            : this(1024)
        { }

        public AsyncSocketAcceptor(Int32 maxConnections)
            : base(maxConnections)
        { }

        protected override void BeginAccept(ListenerContext listener)
        {
            try
            {
                listener.Socket.BeginAccept(AcceptCallback, listener);
            }
            catch (ObjectDisposedException)
            {
                // do nothing
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                ExceptionMonitor.Instance.ExceptionCaught(ex);
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            ListenerContext listener = (ListenerContext)ar.AsyncState;
            System.Net.Sockets.Socket socket;

            try
            {
                socket = listener.Socket.EndAccept(ar);
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            catch (Exception ex)
            {
                ExceptionMonitor.Instance.ExceptionCaught(ex);
                socket = null;
            }

            EndAccept(socket, listener);
        }

        protected override IoSession NewSession(IoProcessor<SocketSession> processor, System.Net.Sockets.Socket socket)
        {
            return new AsyncSocketSession(this, processor, socket, ReuseBuffer);
        }
    }
}
