
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
using FSO.Server.Protocol.Electron.Model;
using Mina.Core.Buffer;

namespace FSO.Server.Protocol.Electron.Packets
{
    public class BulletinRequest : AbstractElectronPacket, IActionRequest
    {
        public BulletinRequestType Type;
        public uint TargetNHood; //the bulletin board to use

        //post message
        public string Title = "";
        public string Message = "";
        public uint LotID; //0 if empty - optionally reference a lot in this bulletin post

        //post message, delete message, promote message
        public uint Value; //bulletin type if post, bulletin ID otherwise.

        public object OType => Type;
        public bool NeedsValidation => false; //the CAN POST items are one off requests, rather than a state machine.

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            Type = input.GetEnum<BulletinRequestType>();
            TargetNHood = input.GetUInt32();

            if (Type == BulletinRequestType.POST_MESSAGE || Type == BulletinRequestType.POST_SYSTEM_MESSAGE)
            {
                Title = input.GetPascalString();
                Message = input.GetPascalString();
                LotID = input.GetUInt32();
            }
            Value = input.GetUInt32();
        }

        public override ElectronPacketType GetPacketType()
        {
            return ElectronPacketType.BulletinRequest;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutEnum(Type);
            output.PutUInt32(TargetNHood);

            if (Type == BulletinRequestType.POST_MESSAGE || Type == BulletinRequestType.POST_SYSTEM_MESSAGE)
            {
                output.PutPascalString(Title);
                output.PutPascalString(Message);
                output.PutUInt32(LotID);
            }
            output.PutUInt32(Value);
        }
    }

    public enum BulletinRequestType : byte
    {
        GET_MESSAGES = 0,
        POST_MESSAGE,
        PROMOTE_MESSAGE, //mayor/admin only.
        CAN_POST_MESSAGE,
        CAN_POST_SYSTEM_MESSAGE,

        //admin
        POST_SYSTEM_MESSAGE,
        DELETE_MESSAGE,
    }
}
