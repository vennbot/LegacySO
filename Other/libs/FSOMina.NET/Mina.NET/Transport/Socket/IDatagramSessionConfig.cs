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

namespace Mina.Transport.Socket
{
    /// <summary>
    /// An <see cref="IoSessionConfig"/> for datagram transport type.
    /// </summary>
    public interface IDatagramSessionConfig : IoSessionConfig
    {
        /// <summary>
        /// <see cref="System.Net.Sockets.Socket.EnableBroadcast"/>
        /// </summary>
        Boolean? EnableBroadcast { get; set; }
        /// <summary>
        /// <see cref="System.Net.Sockets.Socket.ExclusiveAddressUse"/>
        /// </summary>
        Boolean? ExclusiveAddressUse { get; set; }
        /// <summary>
        /// Gets or sets if <see cref="System.Net.Sockets.SocketOptionName.ReuseAddress"/> is enabled.
        /// </summary>
        Boolean? ReuseAddress { get; set; }
        /// <summary>
        /// <see cref="System.Net.Sockets.Socket.ReceiveBufferSize"/>
        /// </summary>
        Int32? ReceiveBufferSize { get; set; }
        /// <summary>
        /// <see cref="System.Net.Sockets.Socket.SendBufferSize"/>
        /// </summary>
        Int32? SendBufferSize { get; set; }
        /// <summary>
        /// Gets or sets traffic class or <see cref="System.Net.Sockets.SocketOptionName.TypeOfService"/> in the IP datagram header.
        /// </summary>
        Int32? TrafficClass { get; set; }
        /// <summary>
        /// Gets or sets <see cref="System.Net.Sockets.MulticastOption"/>.
        /// </summary>
        System.Net.Sockets.MulticastOption MulticastOption { get; set; }
    }
}
