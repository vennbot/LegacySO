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
using System.Collections.Concurrent;
using Mina.Core.Session;

namespace Mina.Core.Write
{
    class DefaultWriteRequestQueue : IWriteRequestQueue
    {
        private ConcurrentQueue<IWriteRequest> q = new ConcurrentQueue<IWriteRequest>();

        public Int32 Size
        {
            get { return q.Count; }
        }

        public IWriteRequest Poll(IoSession session)
        {
            IWriteRequest request;
            q.TryDequeue(out request);
            return request;
        }

        public void Offer(IoSession session, IWriteRequest writeRequest)
        {
            q.Enqueue(writeRequest);
        }

        public Boolean IsEmpty(IoSession session)
        {
            return q.IsEmpty;
        }

        public void Clear(IoSession session)
        {
            q = new ConcurrentQueue<IWriteRequest>();
        }

        public void Dispose(IoSession session)
        {
            // Do nothing
        }
    }
}
