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
using FSO.Server.Protocol.Voltron.Model;
using Mina.Core.Buffer;

namespace FSO.Server.Protocol.Voltron
{
    public abstract class AbstractVoltronPacket : IVoltronPacket
    {
        public static Sender GetSender(IoBuffer buffer)
        {
            var ariesID = buffer.GetPascalString();
            var masterID = buffer.GetPascalString();
            return new Sender { AriesID = ariesID, MasterAccountID = masterID };
        }

        public static void PutSender(IoBuffer buffer, Sender sender)
        {
            buffer.PutPascalString(sender.AriesID);
            buffer.PutPascalString(sender.MasterAccountID);
        }

        public static IoBuffer Allocate(int size)
        {
            IoBuffer buffer = IoBuffer.Allocate(size);
            buffer.Order = ByteOrder.BigEndian;
            return buffer;
        }

        public abstract VoltronPacketType GetPacketType();
        public abstract void Serialize(IoBuffer output, ISerializationContext context);
        public abstract void Deserialize(IoBuffer input, ISerializationContext context);
    }
}
