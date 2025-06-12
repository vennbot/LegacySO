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
using Mina.Core.Buffer;
using Mina.Core.Session;

namespace Mina.Filter.Codec
{
    /// <summary>
    /// Decodes binary or protocol-specific data into higher-level message objects.
    /// </summary>
    public interface IProtocolDecoder
    {
        /// <summary>
        /// Decodes binary or protocol-specific data into higher-level message objects.
        /// </summary>
        void Decode(IoSession session, IoBuffer input, IProtocolDecoderOutput output);
        /// <summary>
        /// Invoked when the specified <tt>session</tt> is closed.  This method is useful
        /// when you deal with the protocol which doesn't specify the length of a message
        /// such as HTTP response without <tt>content-length</tt> header.
        /// </summary>
        void FinishDecode(IoSession session, IProtocolDecoderOutput output);
        /// <summary>
        /// Releases all resources related with this decoder.
        /// </summary>
        void Dispose(IoSession session);
    }
}
