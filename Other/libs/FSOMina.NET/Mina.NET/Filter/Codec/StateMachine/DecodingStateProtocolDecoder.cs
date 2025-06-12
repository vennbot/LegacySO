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
using Mina.Core.Buffer;
using Mina.Core.Session;

namespace Mina.Filter.Codec.StateMachine
{
    /// <summary>
    /// <see cref="IProtocolDecoder"/> which uses a <see cref="IDecodingState"/> to decode data.
    /// Use a <see cref="DecodingStateMachine"/> as <see cref="IDecodingState"/> to create
    /// a state machine which can decode your protocol.
    /// </summary>
    public class DecodingStateProtocolDecoder : IProtocolDecoder
    {
        private readonly IDecodingState _state;
        private readonly ConcurrentQueue<IoBuffer> _undecodedBuffers = new ConcurrentQueue<IoBuffer>();
        private IoSession _session;

        /// <summary>
        /// Creates a new instance using the specified <see cref="IDecodingState"/>.
        /// </summary>
        /// <param name="state">the <see cref="IDecodingState"/></param>
        /// <exception cref="ArgumentNullException">if the specified state is <code>null</code></exception>
        public DecodingStateProtocolDecoder(IDecodingState state)
        {
            if (state == null)
                throw new ArgumentNullException("state");
            _state = state;
        }

        public void Decode(IoSession session, IoBuffer input, IProtocolDecoderOutput output)
        {
            if (_session == null)
                _session = session;
            else if (_session != session)
                throw new InvalidOperationException(GetType().Name + " is a stateful decoder.  "
                        + "You have to create one per session.");

            _undecodedBuffers.Enqueue(input);
            while (true)
            {
                IoBuffer b;
                if (!_undecodedBuffers.TryPeek(out b))
                    break;

                Int32 oldRemaining = b.Remaining;
                _state.Decode(b, output);
                Int32 newRemaining = b.Remaining;
                if (newRemaining != 0)
                {
                    if (oldRemaining == newRemaining)
                        throw new InvalidOperationException(_state.GetType().Name
                            + " must consume at least one byte per decode().");
                }
                else
                {
                    _undecodedBuffers.TryDequeue(out b);
                }
            }
        }

        public void FinishDecode(IoSession session, IProtocolDecoderOutput output)
        {
            _state.FinishDecode(output);
        }

        public void Dispose(IoSession session)
        {
            // Do nothing
        }
    }
}
