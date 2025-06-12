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
using FSO.Common.Serialization;
using Mina.Core.Buffer;

namespace FSO.Server.Protocol.Voltron.Packets
{
    public class HostOnlinePDU : AbstractVoltronPacket
    {
        public ushort HostReservedWords;
        public ushort HostVersion;
        public ushort ClientBufSize = 4096;

        public override VoltronPacketType GetPacketType()
        {
            return VoltronPacketType.HostOnlinePDU;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            //IoBuffer result = Allocate(6);
            output.PutUInt16(HostReservedWords);
            output.PutUInt16(HostVersion);
            output.PutUInt16(ClientBufSize);
            //return result;
        }

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            HostReservedWords = input.GetUInt16();
            HostVersion = input.GetUInt16();
            ClientBufSize = input.GetUInt16();
        }
    }
}
