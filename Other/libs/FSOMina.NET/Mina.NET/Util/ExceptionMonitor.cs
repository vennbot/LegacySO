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
using Common.Logging;

namespace Mina.Util
{
    /// <summary>
    /// Monitors uncaught exceptions.
    /// </summary>
    public abstract class ExceptionMonitor
    {
        private static ExceptionMonitor _instance = DefaultExceptionMonitor.Monitor;

        /// <summary>
        /// Gets or sets the current exception monitor.
        /// </summary>
        public static ExceptionMonitor Instance
        {
            get { return _instance; }
            set { _instance = value ?? DefaultExceptionMonitor.Monitor; }
        }

        /// <summary>
        /// Invoked when there are any uncaught exceptions.
        /// </summary>
        public abstract void ExceptionCaught(Exception cause);
    }

    class DefaultExceptionMonitor : ExceptionMonitor
    {
        public static readonly DefaultExceptionMonitor Monitor = new DefaultExceptionMonitor();
        private static readonly ILog log = LogManager.GetLogger(typeof(DefaultExceptionMonitor));

        public override void ExceptionCaught(Exception cause)
        {
            if (log.IsWarnEnabled)
                log.Warn("Unexpected exception.", cause);
        }
    }
}
