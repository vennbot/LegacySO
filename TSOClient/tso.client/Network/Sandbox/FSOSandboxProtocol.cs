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
using Mina.Filter.Codec;
using Mina.Core.Session;

namespace FSO.Client.Network.Sandbox
{
    public class FSOSandboxProtocol : IProtocolCodecFactory
    {
        private IProtocolDecoder _Decoder;

        public IProtocolDecoder GetDecoder(IoSession session)
        {
            if (_Decoder == null)
            {
                _Decoder = new FSOSandboxProtocolDecoder();
            }
            return _Decoder;
        }

        private IProtocolEncoder _Encoder;

        public IProtocolEncoder GetEncoder(IoSession session)
        {
            if (_Encoder == null)
            {
                _Encoder = new FSOSandboxProtocolEncoder();
            }
            return _Encoder;
        }
    }
}
