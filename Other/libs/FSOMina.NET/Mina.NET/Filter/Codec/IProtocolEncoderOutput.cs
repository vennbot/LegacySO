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
using Mina.Core.Future;

namespace Mina.Filter.Codec
{
    /// <summary>
    /// Callback for <see cref="IProtocolEncoder"/> to generate encoded messages such as
    /// <see cref="Core.Buffer.IoBuffer"/>s.  <see cref="IProtocolEncoder"/> must call #write(Object)
    /// for each encoded message.
    /// </summary>
    public interface IProtocolEncoderOutput
    {
        /// <summary>
        /// Callback for <see cref="IProtocolEncoder"/> to generate encoded messages such as
        /// <see cref="Core.Buffer.IoBuffer"/>s.  <see cref="IProtocolEncoder"/> must call #write(Object)
        /// for each encoded message.
        /// </summary>
        void Write(Object encodedMessage);
        /// <summary>
        /// Merges all buffers you wrote via <see cref="Write(Object)"/> into one <see cref="Core.Buffer.IoBuffer"/>
        /// and replaces the old fragmented ones with it.
        /// This method is useful when you want to control the way Mina.NET generates network packets.
        /// <remarks>
        /// Please note that this method only works when you called <see cref="Write(Object)"/> method
        /// with only <see cref="Core.Buffer.IoBuffer"/>s.
        /// </remarks>
        /// </summary>
        /// <exception cref="InvalidOperationException">if you wrote something else than <see cref="Core.Buffer.IoBuffer"/></exception>
        void MergeAll();
        /// <summary>
        /// Flushes all buffers you wrote via <see cref="Write(Object)"/> to the session.
        /// This operation is asynchronous; please wait for the returned <see cref="IWriteFuture"/>
        /// if you want to wait for the buffers flushed.
        /// </summary>
        /// <returns><code>null</code> if there is nothing to flush at all</returns>
        IWriteFuture Flush();
    }
}
