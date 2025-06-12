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
using FSO.Server.Protocol.Gluon.Model;

namespace FSO.Server.Protocol.Gluon.Packets
{
    public class TransferClaimResponse : AbstractGluonPacket
    {
        public TransferClaimResponseStatus Status;
        public ClaimType Type;
        public int EntityId;
        public uint ClaimId;
        public string NewOwner;

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            Status = input.GetEnum<TransferClaimResponseStatus>();
            Type = input.GetEnum<ClaimType>();
            EntityId = input.GetInt32();
            ClaimId = input.GetUInt32();
            NewOwner = input.GetPascalString();
        }

        public override GluonPacketType GetPacketType()
        {
            return GluonPacketType.TransferClaimResponse;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutEnum(Status);
            output.PutEnum(Type);
            output.PutInt32(EntityId);
            output.PutUInt32(ClaimId);
            output.PutPascalString(NewOwner);
        }
    }

    public enum TransferClaimResponseStatus
    {
        ACCEPTED,
        REJECTED,
        CLAIM_NOT_FOUND
    }
}
