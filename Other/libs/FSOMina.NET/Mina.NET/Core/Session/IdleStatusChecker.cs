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
using System.Collections.Generic;
using System.Threading;

namespace Mina.Core.Session
{
    /// <summary>
    /// Detects idle sessions and fires <tt>SessionIdle</tt> events to them.
    /// </summary>
    public class IdleStatusChecker : IDisposable
    {
        public const Int32 IdleCheckingInterval = 1000;

        private readonly Timer _idleTimer;
        private Int32 _interval;

        public IdleStatusChecker(Func<IEnumerable<IoSession>> getSessionsFunc)
            : this(IdleCheckingInterval, getSessionsFunc)
        { }

        public IdleStatusChecker(Int32 interval, Func<IEnumerable<IoSession>> getSessionsFunc)
        {
            _interval = interval;
            _idleTimer = new Timer(o =>
            {
                AbstractIoSession.NotifyIdleness(getSessionsFunc(), DateTime.Now);
            });
        }

        public Int32 Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        public void Start()
        {
            _idleTimer.Change(0, _interval);
        }

        public void Stop()
        {
            _idleTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                _idleTimer.Dispose();
            }
        }
    }
}
