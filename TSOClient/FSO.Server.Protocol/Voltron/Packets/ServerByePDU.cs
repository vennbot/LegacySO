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
using FSO.Common.Serialization;

namespace FSO.Server.Protocol.Voltron.Packets
{
    public class ServerByePDU : AbstractVoltronPacket
    {
        public uint ReasonCode;
        public string ReasonText;
        public string Ticket;

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            this.ReasonCode = input.GetUInt32();
            this.ReasonText = input.GetPascalString();
            this.Ticket = input.GetPascalString();
        }

        public override VoltronPacketType GetPacketType()
        {
            return VoltronPacketType.ServerByePDU;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            var text = ReasonText;
            if(text == null){
                text = "";
            }
            
            output.PutUInt32(ReasonCode);
            output.PutPascalString(text);
            output.PutPascalString(Ticket);
        }
    }
}
