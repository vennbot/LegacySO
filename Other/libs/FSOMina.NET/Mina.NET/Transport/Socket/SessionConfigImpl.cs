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

namespace Mina.Transport.Socket
{
    class SessionConfigImpl : AbstractSocketSessionConfig
    {
        private readonly System.Net.Sockets.Socket _socket;

        public SessionConfigImpl(System.Net.Sockets.Socket socket)
        {
            _socket = socket;
        }

        public override Int32? ReceiveBufferSize
        {
            get { return _socket.ReceiveBufferSize; }
            set { if (value.HasValue) _socket.ReceiveBufferSize = value.Value; }
        }

        public override Int32? SendBufferSize
        {
            get { return _socket.SendBufferSize; }
            set { if (value.HasValue) _socket.SendBufferSize = value.Value; }
        }

        public override Boolean? ExclusiveAddressUse
        {
            get { return _socket.ExclusiveAddressUse; }
            set { if (value.HasValue) _socket.ExclusiveAddressUse = value.Value; }
        }

        public override Boolean? ReuseAddress
        {
            get
            {
                return Convert.ToBoolean(_socket.GetSocketOption(
                    SocketOptionLevel.Socket, SocketOptionName.ReuseAddress));
            }
            set
            {
                if (value.HasValue)
                    _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, value.Value);
            }
        }

        public override Int32? TrafficClass
        {
            get
            {
                return Convert.ToInt32(_socket.GetSocketOption(
                    SocketOptionLevel.Socket, SocketOptionName.TypeOfService));
            }
            set
            {
                if (value.HasValue)
                    _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.TypeOfService, value.Value);
            }
        }

        public override Boolean? KeepAlive
        {
            get
            {
                return Convert.ToBoolean(_socket.GetSocketOption(
                    SocketOptionLevel.Socket, SocketOptionName.KeepAlive));
            }
            set
            {
                if (value.HasValue)
                    _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, value.Value);
            }
        }

        public override Boolean? OobInline
        {
            get
            {
                return Convert.ToBoolean(_socket.GetSocketOption(
                    SocketOptionLevel.Socket, SocketOptionName.OutOfBandInline));
            }
            set
            {
                if (value.HasValue)
                    _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.OutOfBandInline, value.Value);
            }
        }

        public override Boolean? NoDelay
        {
            get { return _socket.NoDelay; }
            set { if (value.HasValue) _socket.NoDelay = value.Value; }
        }

        public override Int32? SoLinger
        {
            get { return _socket.LingerState.LingerTime; }
            set
            {
                if (value.HasValue)
                {
                    if (value < 0)
                        _socket.LingerState = new LingerOption(false, 0);
                    else
                        _socket.LingerState = new LingerOption(true, value.Value);
                }
            }
        }
    }
}
