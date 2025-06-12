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
using FSO.Common.Enum;

namespace FSO.Server.Protocol.Electron.Packets
{
    public class InstantMessage : AbstractElectronPacket
    {
        public UserReferenceType FromType;
        public uint From;
        public uint To;
        public InstantMessageType Type;
        public string Message;
        public string AckID;
        public InstantMessageFailureReason Reason = InstantMessageFailureReason.NONE;
        public uint Color;

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            FromType = input.GetEnum<UserReferenceType>();
            From = input.GetUInt32();
            To = input.GetUInt32();
            Type = input.GetEnum<InstantMessageType>();
            Message = input.GetPascalVLCString();
            AckID = input.GetPascalVLCString();
            Reason = input.GetEnum<InstantMessageFailureReason>();
            Color = input.GetUInt32();
        }

        public override ElectronPacketType GetPacketType()
        {
            return ElectronPacketType.InstantMessage;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutEnum(FromType);
            output.PutUInt32(From);
            output.PutUInt32(To);
            output.PutEnum(Type);
            output.PutPascalVLCString(Message);
            output.PutPascalVLCString(AckID);
            output.PutEnum(Reason);
            output.PutUInt32(Color);
        }
    }
    
    public enum InstantMessageType
    {
        MESSAGE,
        SUCCESS_ACK,
        FAILURE_ACK
    }

    public enum InstantMessageFailureReason
    {
        NONE,
        THEY_ARE_OFFLINE,
        THEY_ARE_IGNORING_YOU,
        YOU_ARE_IGNORING_THEM,
        MESSAGE_QUEUE_FULL
    }
}
