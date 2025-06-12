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

// Task -> City notifications.

namespace FSO.Server.Protocol.Gluon.Packets
{
    public class CityNotify : AbstractGluonPacket
    {
        public CityNotifyType Mode;
        public uint Value;
        public string Message = "";

        public CityNotify() { }

        public CityNotify(CityNotifyType mode)
        {
            Mode = mode;
        }

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            Mode = input.GetEnum<CityNotifyType>();
            Value = input.GetUInt32();
            Message = input.GetPascalVLCString();
        }

        public override GluonPacketType GetPacketType()
        {
            return GluonPacketType.CityNotify;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutEnum(Mode);
            output.PutUInt32(Value);
            output.PutPascalVLCString(Message);
        }
    }

    public enum CityNotifyType : byte
    {
        NhoodUpdate = 1
    }
}
