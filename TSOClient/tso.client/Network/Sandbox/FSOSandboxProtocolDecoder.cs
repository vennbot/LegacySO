
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
using System.Collections.Generic;
using Mina.Core.Buffer;
using Mina.Core.Session;
using FSO.Common.Serialization;
using FSO.Server.Protocol.Utils;
using FSO.SimAntics.NetPlay.Model;

namespace FSO.Client.Network.Sandbox
{
    public class FSOSandboxProtocolDecoder : CustomCumulativeProtocolDecoder
    {
        protected override bool DoDecode(IoSession session, IoBuffer buffer, IProtocolDecoderOutput output)
        {
            if (buffer.Remaining < 8)
            {
                return false;
            }

            /**
             * We expect aries, voltron or electron packets
             */
            var startPosition = buffer.Position;

            buffer.Order = ByteOrder.LittleEndian;
            uint packetType = buffer.GetUInt32(); //currently unused
            uint payloadSize = buffer.GetUInt32();

            if (buffer.Remaining < payloadSize)
            {
                /** Not all here yet **/
                buffer.Position = startPosition;
                return false;
            }

            var type = (VMNetMessageType)buffer.Get();
            var data = new List<byte>();
            for (int i=0; i<payloadSize-1; i++)
            {
                data.Add(buffer.Get());
            }
            var packet = new VMNetMessage(type, data.ToArray());
            output.Write(packet);

            return true;
        }
    }
}
