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

namespace Mina.Core.Write
{
    /// <summary>
    /// Represents write request fired by <see cref="Core.Session.IoSession.Write(Object)"/>.
    /// </summary>
    public interface IWriteRequest
    {
        /// <summary>
        /// Gets the <see cref="IWriteRequest"/> which was requested originally,
        /// which is not transformed by any <see cref="Core.Filterchain.IoFilter"/>.
        /// </summary>
        IWriteRequest OriginalRequest { get; }
        /// <summary>
        /// Gets the message object to be written.
        /// </summary>
        Object Message { get; }
        /// <summary>
        /// Gets the destination of this write request.
        /// </summary>
        EndPoint Destination { get; }
        /// <summary>
        /// Tells if the current message has been encoded.
        /// </summary>
        Boolean Encoded { get; }
        /// <summary>
        /// Gets <see cref="IWriteFuture"/> that is associated with this write request.
        /// </summary>
        IWriteFuture Future { get; }
    }
}
