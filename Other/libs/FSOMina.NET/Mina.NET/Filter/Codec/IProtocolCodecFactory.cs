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
using Mina.Core.Session;

namespace Mina.Filter.Codec
{
    /// <summary>
    /// Provides <see cref="IProtocolEncoder"/> and <see cref="IProtocolDecoder"/> which translates
    /// binary or protocol specific data into message object and vice versa.
    /// </summary>
    public interface IProtocolCodecFactory
    {
        /// <summary>
        /// Returns a new (or reusable) instance of <see cref="IProtocolEncoder"/> which
        /// encodes message objects into binary or protocol-specific data.
        /// </summary>
        IProtocolEncoder GetEncoder(IoSession session);
        /// <summary>
        /// Returns a new (or reusable) instance of <see cref="IProtocolDecoder"/> which
        /// decodes binary or protocol-specific data into message objects.
        /// </summary>
        IProtocolDecoder GetDecoder(IoSession session);
    }
}
