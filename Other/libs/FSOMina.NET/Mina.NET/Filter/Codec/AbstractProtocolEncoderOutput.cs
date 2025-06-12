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
using Mina.Util;
using Mina.Core.Future;
using Mina.Core.Buffer;

namespace Mina.Filter.Codec
{
    /// <summary>
    /// A <see cref="IProtocolEncoderOutput"/> based on queue.
    /// </summary>
    public abstract class AbstractProtocolEncoderOutput : IProtocolEncoderOutput
    {
        private readonly IQueue<Object> _queue = new ConcurrentQueue<Object>();
        private Boolean _buffersOnly = true;

        public IQueue<Object> MessageQueue
        {
            get { return _queue; }
        }

        /// <inheritdoc/>
        public void Write(Object encodedMessage)
        {
            IoBuffer buf = encodedMessage as IoBuffer;
            if (buf == null)
            {
                _buffersOnly = false;
            }
            else if (!buf.HasRemaining)
            {
                throw new ArgumentException("buf is empty. Forgot to call flip()?");
            }
            _queue.Enqueue(encodedMessage);
        }

        /// <inheritdoc/>
        public void MergeAll()
        {
            if (!_buffersOnly)
                throw new InvalidOperationException("The encoded messages contains a non-buffer.");

            if (_queue.Count < 2)
                // no need to merge!
                return;

            Int32 sum = 0;
            foreach (var item in _queue)
            {
                sum += ((IoBuffer)item).Remaining;
            }

            IoBuffer newBuf = IoBuffer.Allocate(sum);
            for (; ; )
            {
                Object obj = _queue.Dequeue();
                if (obj == null)
                    break;
                newBuf.Put((IoBuffer)obj);
            }

            newBuf.Flip();
            _queue.Enqueue(newBuf);
        }

        /// <inheritdoc/>
        public abstract IWriteFuture Flush();
    }
}
