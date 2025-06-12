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
    /// A convenience <see cref="IProtocolCodecFactory"/> that provides <see cref="DemuxingProtocolEncoder"/>
    /// and <see cref="DemuxingProtocolDecoder"/> as a pair.
    /// <remarks>
    /// <see cref="DemuxingProtocolEncoder"/> and <see cref="DemuxingProtocolDecoder"/> demultiplex
    /// incoming messages and buffers to appropriate <see cref="IMessageEncoder"/>s and 
    /// <see cref="IMessageDecoder"/>s.
    /// </remarks>
    /// </summary>
    public class DemuxingProtocolCodecFactory : IProtocolCodecFactory
    {
        internal static readonly Type[] EmptyParams = new Type[0];

        private readonly DemuxingProtocolEncoder encoder = new DemuxingProtocolEncoder();
        private readonly DemuxingProtocolDecoder decoder = new DemuxingProtocolDecoder();

        /// <inheritdoc/>
        public IProtocolEncoder GetEncoder(IoSession session)
        {
            return encoder;
        }

        /// <inheritdoc/>
        public IProtocolDecoder GetDecoder(IoSession session)
        {
            return decoder;
        }

        public void AddMessageEncoder<TMessage, TEncoder>() where TEncoder : IMessageEncoder
        {
            this.encoder.AddMessageEncoder<TMessage, TEncoder>();
        }

        public void AddMessageEncoder<TMessage>(IMessageEncoder<TMessage> encoder)
        {
            this.encoder.AddMessageEncoder<TMessage>(encoder);
        }

        public void AddMessageEncoder<TMessage>(IMessageEncoderFactory<TMessage> factory)
        {
            this.encoder.AddMessageEncoder<TMessage>(factory);
        }

        public void AddMessageDecoder<TDecoder>() where TDecoder : IMessageDecoder
        {
            this.decoder.AddMessageDecoder<TDecoder>();
        }

        public void AddMessageDecoder(IMessageDecoder decoder)
        {
            this.decoder.AddMessageDecoder(decoder);
        }

        public void AddMessageDecoder(IMessageDecoderFactory factory)
        {
            this.decoder.AddMessageDecoder(factory);
        }
    }
}
