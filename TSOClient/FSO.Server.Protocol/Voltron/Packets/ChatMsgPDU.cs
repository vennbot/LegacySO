
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
using FSO.Server.Protocol.Voltron.Model;

namespace FSO.Server.Protocol.Voltron.Packets
{
    public class ChatMsgPDU : AbstractVoltronPacket
    {
        public bool EchoRequested;
        public Sender Sender;
        public byte Badge;
        public byte Alertable;
        public string Message;

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            EchoRequested = input.Get() == 0x01;
            Sender = GetSender(input);
            Badge = input.Get();
            Alertable = input.Get();
            Message = input.GetPascalString();
        }

        public override VoltronPacketType GetPacketType()
        {
            return VoltronPacketType.ChatMsgPDU;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.Put(EchoRequested ? (byte)1 : (byte)0);
            PutSender(output, Sender);
            output.Put(Badge);
            output.Put(Alertable);
            output.PutPascalString(Message);
        }
    }
}
