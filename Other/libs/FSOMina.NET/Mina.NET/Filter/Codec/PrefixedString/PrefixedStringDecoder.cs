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
using System.Text;
using Mina.Core.Buffer;
using Mina.Core.Session;

namespace Mina.Filter.Codec.PrefixedString
{
    /// <summary>
    /// A <see cref="IProtocolDecoder"/> which decodes a String using a fixed-length length prefix.
    /// </summary>
    public class PrefixedStringDecoder : CumulativeProtocolDecoder
    {
        public PrefixedStringDecoder(Encoding encoding)
            : this(encoding, PrefixedStringCodecFactory.DefaultPrefixLength, PrefixedStringCodecFactory.DefaultMaxDataLength)
        { }

        public PrefixedStringDecoder(Encoding encoding, Int32 prefixLength)
            : this(encoding, prefixLength, PrefixedStringCodecFactory.DefaultMaxDataLength)
        { }

        public PrefixedStringDecoder(Encoding encoding, Int32 prefixLength, Int32 maxDataLength)
        {
            Encoding = encoding;
            PrefixLength = prefixLength;
            MaxDataLength = maxDataLength;
        }

        /// <summary>
        /// Gets or sets the length of the length prefix (1, 2, or 4).
        /// </summary>
        public Int32 PrefixLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum allowed value specified as data length in the incoming data.
        /// </summary>
        public Int32 MaxDataLength { get; set; }

        /// <summary>
        /// Gets or set the text encoding.
        /// </summary>
        public Encoding Encoding { get; set; }

        protected override Boolean DoDecode(IoSession session, IoBuffer input, IProtocolDecoderOutput output)
        {
            if (input.PrefixedDataAvailable(PrefixLength, MaxDataLength))
            {
                String msg = input.GetPrefixedString(PrefixLength, Encoding);
                output.Write(msg);
                return true;
            }

            return false;
        }
    }
}
