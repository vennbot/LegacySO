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
using Mina.Core.Write;

namespace Mina.Core.Filterchain
{
    /// <summary>
    /// Represents the next <see cref="IoFilter"/> in <see cref="IoFilterChain"/>.
    /// </summary>
    public interface INextFilter
    {
        /// <summary>
        /// Forwards <code>SessionCreated</code> event to next filter.
        /// </summary>
        void SessionCreated(IoSession session);
        /// <summary>
        /// Forwards <code>SessionOpened</code> event to next filter.
        /// </summary>
        void SessionOpened(IoSession session);
        /// <summary>
        /// Forwards <code>SessionClosed</code> event to next filter.
        /// </summary>
        void SessionClosed(IoSession session);
        /// <summary>
        /// Forwards <code>SessionIdle</code> event to next filter.
        /// </summary>
        void SessionIdle(IoSession session, IdleStatus status);
        /// <summary>
        /// Forwards <code>ExceptionCaught</code> event to next filter.
        /// </summary>
        void ExceptionCaught(IoSession session, Exception cause);
        /// <summary>
        /// Forwards <code>InputClosed</code> event to next filter.
        /// </summary>
        void InputClosed(IoSession session);
        /// <summary>
        /// Forwards <code>MessageReceived</code> event to next filter.
        /// </summary>
        void MessageReceived(IoSession session, Object message);
        /// <summary>
        /// Forwards <code>MessageSent</code> event to next filter.
        /// </summary>
        void MessageSent(IoSession session, IWriteRequest writeRequest);
        /// <summary>
        /// Forwards <code>FilterClose</code> event to next filter.
        /// </summary>
        void FilterClose(IoSession session);
        /// <summary>
        /// Forwards <code>FilterWrite</code> event to next filter.
        /// </summary>
        void FilterWrite(IoSession session, IWriteRequest writeRequest);
    }
}
