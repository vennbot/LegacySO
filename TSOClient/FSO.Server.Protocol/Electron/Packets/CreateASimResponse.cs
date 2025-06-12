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

namespace FSO.Server.Protocol.Electron.Packets
{
    public class CreateASimResponse : AbstractElectronPacket
    {
        public CreateASimStatus Status { get; set; }
        public CreateASimFailureReason Reason { get; set; } = CreateASimFailureReason.NONE;
        public uint NewAvatarId { get; set; }

        public override ElectronPacketType GetPacketType()
        {
            return ElectronPacketType.CreateASimResponse;
        }

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            Status = input.GetEnum<CreateASimStatus>();
            Reason = input.GetEnum<CreateASimFailureReason>();
            NewAvatarId = input.GetUInt32();
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutEnum<CreateASimStatus>(Status);
            output.PutEnum<CreateASimFailureReason>(Reason);
            output.PutUInt32(NewAvatarId);
        }
    }

    public enum CreateASimStatus
    {
        SUCCESS = 0x01,
        FAILED = 0x02
    }

    public enum CreateASimFailureReason
    {
        NONE = 0x00,
        NAME_TAKEN = 0x01,
        NAME_VALIDATION_ERROR = 0x02,
        DESC_VALIDATION_ERROR = 0x03,
        BODY_VALIDATION_ERROR = 0x04,
        HEAD_VALIDATION_ERROR = 0x05
    }
}
