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

namespace Mina.Filter.Codec
{
    /// <summary>
    /// A <see cref="IProtocolEncoder"/> implementation which decorates an existing encoder
    /// to be thread-safe.  Please be careful if you're going to use this decorator
    /// because it can be a root of performance degradation in a multi-thread
    /// environment.  Please use this decorator only when you need to synchronize
    /// on a per-encoder basis instead of on a per-session basis, which is not
    /// common.
    /// </summary>
    public class SynchronizedProtocolEncoder : IProtocolEncoder
    {
        private readonly IProtocolEncoder _encoder;

        public SynchronizedProtocolEncoder(IProtocolEncoder encoder)
        {
            if (encoder == null)
                throw new ArgumentNullException("encoder");
            _encoder = encoder;
        } 

        public void Encode(IoSession session, Object message, IProtocolEncoderOutput output)
        {
            lock (_encoder)
            {
                _encoder.Encode(session, message, output);
            }
        }

        public void Dispose(IoSession session)
        {
            lock (_encoder)
            {
                _encoder.Dispose(session);
            }
        }
    }
}
