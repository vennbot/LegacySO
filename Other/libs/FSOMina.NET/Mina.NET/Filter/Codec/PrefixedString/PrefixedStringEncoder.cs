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
    /// A <see cref="IProtocolEncoder"/> which encodes a string using a fixed-length length prefix.
    /// </summary>
    public class PrefixedStringEncoder : ProtocolEncoderAdapter
    {
         public PrefixedStringEncoder(Encoding encoding)
            : this(encoding, PrefixedStringCodecFactory.DefaultPrefixLength, PrefixedStringCodecFactory.DefaultMaxDataLength)
        { }

        public PrefixedStringEncoder(Encoding encoding, Int32 prefixLength)
             : this(encoding, prefixLength, PrefixedStringCodecFactory.DefaultMaxDataLength)
        { }

        public PrefixedStringEncoder(Encoding encoding, Int32 prefixLength, Int32 maxDataLength)
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
        /// Gets or sets the maximum number of bytes allowed for encoding a single string.
        /// </summary>
        public Int32 MaxDataLength { get; set; }

        /// <summary>
        /// Gets or set the text encoding.
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <inheritdoc/>
        public override void Encode(IoSession session, Object message, IProtocolEncoderOutput output)
        {
            String value = (String)message;
            IoBuffer buffer = IoBuffer.Allocate(value.Length);
            buffer.AutoExpand = true;
            buffer.PutPrefixedString(value, PrefixLength, Encoding);
            if (buffer.Position > MaxDataLength)
            {
                throw new ArgumentException("Data length: " + buffer.Position);
            }
            buffer.Flip();
            output.Write(buffer);
        }
    }
}
