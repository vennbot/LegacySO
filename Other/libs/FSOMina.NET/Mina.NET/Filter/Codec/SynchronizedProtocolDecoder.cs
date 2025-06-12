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
using Mina.Core.Buffer;
using Mina.Core.Session;

namespace Mina.Filter.Codec
{
    /// <summary>
    /// A <see cref="IProtocolDecoder"/> implementation which decorates an existing decoder
    /// to be thread-safe.  Please be careful if you're going to use this decorator
    /// because it can be a root of performance degradation in a multi-thread
    /// environment.  Also, by default, appropriate synchronization is done
    /// on a per-session basis by <see cref="ProtocolCodecFilter"/>.  Please use this
    /// decorator only when you need to synchronize on a per-decoder basis, which
    /// is not common.
    /// </summary>
    public class SynchronizedProtocolDecoder : IProtocolDecoder
    {
        private readonly IProtocolDecoder _decoder;

        public SynchronizedProtocolDecoder(IProtocolDecoder decoder)
        {
            if (decoder == null)
                throw new ArgumentNullException("decoder");
            _decoder = decoder;
        } 

        public void Decode(IoSession session, IoBuffer input, IProtocolDecoderOutput output)
        {
            lock (_decoder)
            {
                _decoder.Decode(session, input, output);
            }
        }

        public void FinishDecode(IoSession session, IProtocolDecoderOutput output)
        {
            lock (_decoder)
            {
                _decoder.FinishDecode(session, output);
            }
        }

        public void Dispose(IoSession session)
        {
            lock (_decoder)
            {
                _decoder.Dispose(session);
            }
        }
    }
}
