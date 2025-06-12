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

namespace Mina.Filter.Codec.Demux
{
    /// <summary>
    /// Encodes a certain type of messages.
    /// </summary>
    public interface IMessageEncoder
    {
        /// <summary>
        /// Encodes higher-level message objects into binary or protocol-specific data.
        /// </summary>
        /// <remarks>
        /// MINA invokes <code>Encode(IoSession, Object, ProtocolEncoderOutput)</code>
        /// method with message which is popped from the session write queue, and then
        /// the encoder implementation puts encoded <see cref="Core.Buffer.IoBuffer"/>s into
        /// <see cref="IProtocolEncoderOutput"/>.
        /// </remarks>
        void Encode(IoSession session, Object message, IProtocolEncoderOutput output);
    }

    /// <summary>
    /// Encodes a certain type of messages.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMessageEncoder<T> : IMessageEncoder
    {
        /// <summary>
        /// Encodes higher-level message objects into binary or protocol-specific data.
        /// </summary>
        /// <remarks>
        /// MINA invokes <code>Encode(IoSession, Object, ProtocolEncoderOutput)</code>
        /// method with message which is popped from the session write queue, and then
        /// the encoder implementation puts encoded <see cref="Core.Buffer.IoBuffer"/>s into
        /// <see cref="IProtocolEncoderOutput"/>.
        /// </remarks>
        void Encode(IoSession session, T message, IProtocolEncoderOutput output);
    }
}
