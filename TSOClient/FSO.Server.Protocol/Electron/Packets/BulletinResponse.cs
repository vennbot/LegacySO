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
using FSO.Files.Formats.tsodata;
using FSO.Server.Protocol.Electron.Model;
using Mina.Core.Buffer;
using System.IO;

namespace FSO.Server.Protocol.Electron.Packets
{
    public class BulletinResponse : AbstractElectronPacket, IActionResponse
    {
        public BulletinResponseType Type;
        public BulletinItem[] Messages = new BulletinItem[0];
        public string Message;
        public uint BanEndDate;

        public bool Success => Type == BulletinResponseType.SUCCESS || Type == BulletinResponseType.SEND_SUCCESS || Type == BulletinResponseType.MESSAGES;
        public object OCode => Type;

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            Type = input.GetEnum<BulletinResponseType>();
            var numMessages = input.GetInt32();
            Messages = new BulletinItem[numMessages];
            for (int j = 0; j < numMessages; j++)
            {
                var length = input.GetInt32();
                var dat = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    dat[i] = input.Get();
                }

                using (var str = new MemoryStream(dat))
                {
                    Messages[j] = new BulletinItem(str);
                }
            }
            Message = input.GetPascalVLCString();
            BanEndDate = input.GetUInt32();
        }

        public override ElectronPacketType GetPacketType()
        {
            return ElectronPacketType.BulletinResponse;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutEnum(Type);
            output.PutInt32(Messages.Length);
            foreach (var msg in Messages)
            {
                byte[] dat;
                using (var str = new MemoryStream())
                {
                    msg.Save(str);
                    dat = str.ToArray();
                }
                output.PutInt32(dat.Length);
                foreach (var b in dat)
                    output.Put(b);
            }
            output.PutPascalVLCString(Message);
            output.PutUInt32(BanEndDate);
        }
    }

    public enum BulletinResponseType
    {
        MESSAGES,
        SEND_SUCCESS, //returns message you sent, with dbid
        SUCCESS,
        SEND_FAIL_NON_RESIDENT,
        SEND_FAIL_BAD_PERMISSION,
        SEND_FAIL_INVALID_LOT,
        SEND_FAIL_INVALID_MESSAGE,
        SEND_FAIL_INVALID_TITLE,
        SEND_FAIL_GAMEPLAY_BAN,
        SEND_FAIL_TOO_FREQUENT,
        SEND_FAIL_JUST_MOVED,
        FAIL_MESSAGE_DOESNT_EXIST,
        FAIL_NOT_MAYOR,
        FAIL_CANT_DELETE,
        FAIL_ALREADY_PROMOTED,
        FAIL_BAD_PERMISSION,

        CANCEL = 0xFE,
        FAIL_UNKNOWN = 0xFF
    }
}
