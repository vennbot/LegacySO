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
    /// An exception that is thrown when <see cref="IProtocolEncoder"/> or
    /// <see cref="IProtocolDecoder"/> cannot understand or failed to validate
    /// data to process.
    /// </summary>
    [Serializable]
    public class ProtocolCodecException : Exception
    {
        /// <summary>
        /// </summary>
        public ProtocolCodecException()
        { }

        /// <summary>
        /// </summary>
        public ProtocolCodecException(String message)
            : base(message)
        { }

        /// <summary>
        /// </summary>
        public ProtocolCodecException(String message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// </summary>
        protected ProtocolCodecException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    /// <summary>
    /// An exception that is thrown when <see cref="IProtocolEncoder"/>
    /// cannot understand or failed to validate the specified message object.
    /// </summary>
    [Serializable]
    public class ProtocolEncoderException : ProtocolCodecException
    {
        /// <summary>
        /// </summary>
        public ProtocolEncoderException()
        { }

        /// <summary>
        /// </summary>
        public ProtocolEncoderException(String message)
            : base(message)
        { }

        /// <summary>
        /// </summary>
        public ProtocolEncoderException(String message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// </summary>
        protected ProtocolEncoderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    /// <summary>
    /// An exception that is thrown when <see cref="IProtocolDecoder"/>
    /// cannot understand or failed to validate the specified <see cref="Core.Buffer.IoBuffer"/>
    /// content.
    /// </summary>
    [Serializable]
    public class ProtocolDecoderException : ProtocolCodecException
    {
        private String _hexdump;

        /// <summary>
        /// </summary>
        public ProtocolDecoderException()
        { }

        /// <summary>
        /// </summary>
        public ProtocolDecoderException(String message)
            : base(message)
        { }

        /// <summary>
        /// </summary>
        public ProtocolDecoderException(String message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// </summary>
        protected ProtocolDecoderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// Gets the current data in hex.
        /// </summary>
        public String Hexdump
        {
            get { return _hexdump; }
            set
            {
                if (_hexdump != null)
                    throw new InvalidOperationException("Hexdump cannot be set more than once.");
                _hexdump = value;
            }
        }

        /// <summary>
        /// </summary>
        public override void GetObjectData(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
