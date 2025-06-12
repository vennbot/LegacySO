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
using Mina.Core.Service;
using System;
using Mina.Core.Session;
using System.Net;
using System.Globalization;
using Mina.Transport.Socket;
using Mina.Core.Future;
using Mina.Filter.Codec;
using FSO.SimAntics.NetPlay.Model;
using FSO.Common.Utils;

namespace FSO.Client.Network.Sandbox
{
    public class FSOSandboxClient : IoHandler
    {
        private IoConnector Connector;
        private IoSession Session;

        public event Action<VMNetMessage> OnMessage;
        public event Action OnConnectComplete;

        public void Connect(string address)
        {
            Connect(CreateIPEndPoint(address));
        }

        public void Disconnect()
        {
            if (Session != null)
            {
                Session.Close(false);
            }
        }

        public void Connect(IPEndPoint target)
        {
            var socketConnector = new AsyncSocketConnector();
            socketConnector.SessionConfig.NoDelay = true;
            Connector = socketConnector;
            Connector.ConnectTimeoutInMillis = 10000;

            Connector.Handler = this;
            Connector.FilterChain.AddLast("protocol", new ProtocolCodecFilter(new FSOSandboxProtocol()));
            Connector.Connect(target, new Action<IoSession, IConnectFuture>(OnConnect));
        }

        private void OnConnect(IoSession session, IConnectFuture future)
        {
            this.Session = session;
            GameThread.NextUpdate(x =>
            {
                OnConnectComplete();
            });
        }

        public void Write(params object[] packets)
        {
            if (this.Session != null)
            {
                this.Session.Write(packets);
            }
        }

        public bool IsConnected
        {
            get
            {
                return Session != null && Session.Connected;
            }
        }

        public static IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length != 2) throw new FormatException("Invalid endpoint format");
            System.Net.IPAddress ip;
            if (!System.Net.IPAddress.TryParse(ep[0], out ip))
            {
                var addrs = Dns.GetHostEntry(ep[0]).AddressList;
                if (addrs.Length == 0)
                {
                    throw new FormatException("Invalid ip-address");
                }
                else ip = addrs[0];
            }

            int port;
            if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }
            return new IPEndPoint(ip, port);
        }

        public void ExceptionCaught(IoSession session, Exception cause)
        {
        }

        public void InputClosed(IoSession session)
        {
        }

        public void MessageReceived(IoSession session, object message)
        {
            if (message is VMNetMessage)
            {
                var nmsg = (VMNetMessage)message;
                OnMessage(nmsg);
            }
        }

        public void MessageSent(IoSession session, object message)
        {
        }

        public void SessionClosed(IoSession session)
        {
        }

        public void SessionCreated(IoSession session)
        {
        }

        public void SessionIdle(IoSession session, IdleStatus status)
        {
        }

        public void SessionOpened(IoSession session)
        {
        }
    }
}
