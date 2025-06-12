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
    /// <code>ExceptionCaught</code> events to.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Invoked when the specific type of exception is caught from the
        /// specified <code>session</code>.
        /// </summary>
        void ExceptionCaught(IoSession session, Exception cause);
    }

    /// <summary>
    /// A handler interface that <see cref="DemuxingIoHandler"/> forwards
    /// <code>ExceptionCaught</code> events to.
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public interface IExceptionHandler<in E> : IExceptionHandler where E : Exception
    {
        /// <summary>
        /// Invoked when the specific type of exception is caught from the
        /// specified <code>session</code>.
        /// </summary>
        void ExceptionCaught(IoSession session, E cause);
    }
}
