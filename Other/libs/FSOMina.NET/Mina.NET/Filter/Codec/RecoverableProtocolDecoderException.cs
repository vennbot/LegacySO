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

namespace Mina.Filter.Codec
{
    /// <summary>
    /// A special exception that tells the <see cref="IProtocolDecoder"/> can keep
    /// decoding even after this exception is thrown.
    /// </summary>
    [Serializable]
    public class RecoverableProtocolDecoderException : ProtocolDecoderException
    {
        public RecoverableProtocolDecoderException() { }

        public RecoverableProtocolDecoderException(String message)
            : base(message) { }

        public RecoverableProtocolDecoderException(String message, Exception innerException)
            : base(message, innerException) { }

        protected RecoverableProtocolDecoderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
