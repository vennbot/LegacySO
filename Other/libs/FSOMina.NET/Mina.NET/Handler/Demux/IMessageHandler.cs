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

namespace Mina.Handler.Demux
{
    /// <summary>
    /// A handler interface that <see cref="DemuxingIoHandler"/> forwards
    /// <tt>MessageReceived</tt> or <tt>MessageSent</tt> events to.
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// Invoked when the specific type of message is received from or sent to
        /// the specified <code>session</code>.
        /// </summary>
        /// <param name="session">the associated <see cref="IoSession"/></param>
        /// <param name="message">the message to decode</param>
        void HandleMessage(IoSession session, Object message);
    }

    /// <summary>
    /// A handler interface that <see cref="DemuxingIoHandler"/> forwards
    /// <tt>MessageReceived</tt> or <tt>MessageSent</tt> events to.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMessageHandler<in T> : IMessageHandler
    {
        /// <summary>
        /// Invoked when the specific type of message is received from or sent to
        /// the specified <code>session</code>.
        /// </summary>
        /// <param name="session">the associated <see cref="IoSession"/></param>
        /// <param name="message">the message to decode. Its type is set by the implementation</param>
        void HandleMessage(IoSession session, T message);
    }
}
