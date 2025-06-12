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
using System.Net;
using Mina.Core.Service;
using Mina.Core.Session;

namespace Mina.Transport.Socket
{
    /// <summary>
    /// <see cref="IoAcceptor"/> for socket transport (TCP/IP).  This class handles incoming TCP/IP based socket connections.
    /// </summary>
    public interface IDatagramAcceptor : IoAcceptor
    {
        /// <inheritdoc/>
        new IDatagramSessionConfig SessionConfig { get; }
        /// <inheritdoc/>
        new IPEndPoint LocalEndPoint { get; }
        /// <inheritdoc/>
        new IPEndPoint DefaultLocalEndPoint { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="IoSessionRecycler"/> for this service.
        /// </summary>
        IoSessionRecycler SessionRecycler { get; set; }
    }
}
