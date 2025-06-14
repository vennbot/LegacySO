
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
using System;
using Mina.Core.Buffer;
using FSO.Common.Serialization;
using FSO.Server.Protocol.Gluon.Packets;

namespace FSO.Server.Protocol.Gluon
{
    public abstract class AbstractGluonPacket : IGluonPacket
    {
        public static IoBuffer Allocate(int size)
        {
            IoBuffer buffer = IoBuffer.Allocate(size);
            buffer.Order = ByteOrder.BigEndian;
            return buffer;
        }

        public abstract GluonPacketType GetPacketType();
        public abstract void Deserialize(IoBuffer input, ISerializationContext context);
        public abstract void Serialize(IoBuffer output, ISerializationContext context);
    }

    public abstract class AbstractGluonCallPacket : AbstractGluonPacket, IGluonCall
    {
        public Guid CallId { get; set; }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutPascalString(CallId.ToString());
        }

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            CallId = Guid.Parse(input.GetPascalString());
        }
    }
}
