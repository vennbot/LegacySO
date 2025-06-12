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
using Mina.Util;

namespace Mina.Core.Session
{
    /// <summary>
    /// An <see cref="IoSessionRecycler"/> with sessions that time out on inactivity.
    /// </summary>
    public class ExpiringSessionRecycler : IoSessionRecycler
    {
        private readonly ExpiringMap<EndPoint, IoSession> _sessionMap;

        public ExpiringSessionRecycler()
            : this(new ExpiringMap<EndPoint, IoSession>())
        { }

        public ExpiringSessionRecycler(Int32 timeToLive)
            : this(new ExpiringMap<EndPoint, IoSession>(timeToLive))
        { }

        public ExpiringSessionRecycler(Int32 timeToLive, Int32 expirationInterval)
            : this(new ExpiringMap<EndPoint, IoSession>(timeToLive, expirationInterval))
        { }

        private ExpiringSessionRecycler(ExpiringMap<EndPoint, IoSession> map)
        {
            _sessionMap = map;
            _sessionMap.Expired += new EventHandler<ExpirationEventArgs<IoSession>>(_sessionMap_Expired);
        }

        void _sessionMap_Expired(object sender, ExpirationEventArgs<IoSession> e)
        {
            e.Object.Close(true);
        }

        /// <inheritdoc/>
        public void Put(IoSession session)
        {
            _sessionMap.StartExpiring();
            EndPoint key = session.RemoteEndPoint;
            if (!_sessionMap.ContainsKey(key))
                _sessionMap.Add(key, session);
        }

        /// <inheritdoc/>
        public IoSession Recycle(EndPoint remoteEP)
        {
            return _sessionMap[remoteEP];
        }

        /// <inheritdoc/>
        public void Remove(IoSession session)
        {
            _sessionMap.Remove(session.RemoteEndPoint);
        }

        public void StopExpiring()
        {
            _sessionMap.StopExpiring();
        }

        public Int32 ExpirationInterval
        {
            get { return _sessionMap.ExpirationInterval; }
            set { _sessionMap.ExpirationInterval = value; }
        }

        public Int32 TimeToLive
        {
            get { return _sessionMap.TimeToLive; }
            set { _sessionMap.TimeToLive = value; }
        }
    }
}
