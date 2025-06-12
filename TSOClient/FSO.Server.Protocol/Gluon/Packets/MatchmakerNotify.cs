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

namespace FSO.Server.Protocol.Gluon.Packets
{
    /// <summary>
    /// Lot -> City server messages used to notify the matchmaker about some change to lot state.
    /// (currently only when an avatar leaves a lot. this frees up a space for the matchmaker to shove someone else in)
    /// </summary>
    public class MatchmakerNotify : AbstractGluonPacket
    {
        public MatchmakerNotifyType Mode;
        public uint LotID;
        public uint AvatarID; 

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            Mode = input.GetEnum<MatchmakerNotifyType>();
            LotID = input.GetUInt32();
            AvatarID = input.GetUInt32();
        }

        public override GluonPacketType GetPacketType()
        {
            return GluonPacketType.MatchmakerNotify;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutEnum(Mode);
            output.PutUInt32(LotID);
            output.PutUInt32(AvatarID);
        }
    }

    public enum MatchmakerNotifyType : byte
    {
        RemoveAvatar = 1
    }
}
