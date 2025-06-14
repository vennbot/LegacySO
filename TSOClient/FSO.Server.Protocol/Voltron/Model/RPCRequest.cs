
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
using FSO.Common.Serialization;
using Mina.Core.Buffer;
using System;

namespace FSO.Server.Protocol.Voltron.Model
{
    /// <summary>
    /// DBRequestWrapperPDU
    /// </summary>
    public class RPCRequest
    {
        public object[] Parameters { get; set; }
        

        public RPCRequest(IoBuffer buffer)
        {
            var bodyType = buffer.GetUInt32();

            switch (bodyType)
            {
                case 0x125194E5:
                    ParseFormat1(buffer);
                    break;
                default:
                    throw new Exception("Unknown RPC request type");
            }
        }

        private void ParseFormat1(IoBuffer buffer)
        {
            var unknown = buffer.GetUInt32();
            var sendingAvatarId = buffer.GetUInt32();
            var flags = (byte)buffer.Get();
            var messageId = buffer.GetUInt32();

            if ((((flags) >> 1) & 0x01) == 0x01)
            {
                var unknown2 = buffer.GetUInt32();
            }

            if ((((flags) >> 2) & 0x01) == 0x01)
            {
                var parameter = new byte[4];
                buffer.Get(parameter, 0, 4);
            }

            if ((((flags) >> 3) & 0x01) == 0x01)
            {
                var unknown3 = buffer.GetUInt32();
            }

            if ((((flags) >> 5) & 0x01) == 0x01)
            {
                var requestResponseID = buffer.GetUInt32();

                /** Variable bytes **/

            }
        }
    }
}
