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
using Mina.Core.Service;

namespace Mina.Transport.Socket
{
    /// <summary>
    /// <see cref="IoAcceptor"/> for socket transport (TCP/IP).  This class handles incoming TCP/IP based socket connections.
    /// </summary>
    public interface ISocketAcceptor : IoAcceptor
    {
        /// <inheritdoc/>
        new ISocketSessionConfig SessionConfig { get; }
        /// <inheritdoc/>
        new IPEndPoint LocalEndPoint { get; }
        /// <inheritdoc/>
        new IPEndPoint DefaultLocalEndPoint { get; set; }
        /// <summary>
        /// Gets or sets the Reuse Address flag.
        /// </summary>
        Boolean ReuseAddress { get; set; }
        /// <summary>
        /// Gets or sets the size of the backlog. This can only be set when this class is not bound.
        /// </summary>
        Int32 Backlog { get; set; }
    }
}
