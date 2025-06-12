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
using FSO.Server.Protocol.Voltron.Model;
using FSO.Common.Serialization;

namespace FSO.Server.Protocol.Voltron.Packets
{
    public class OccupantArrivedPDU : AbstractVoltronPacket
    {
        public Sender Sender;
        public byte Badge;
        public bool IsAlertable;

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            Sender = GetSender(input);
            Badge = input.Get();
            IsAlertable = input.Get() == 0x1;
        }

        public override VoltronPacketType GetPacketType()
        {
            return VoltronPacketType.OccupantArrivedPDU;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            PutSender(output, Sender);
            output.Put(Badge);
            output.Put((IsAlertable ? (byte)0x01 : (byte)0x00));
        }
    }
}
