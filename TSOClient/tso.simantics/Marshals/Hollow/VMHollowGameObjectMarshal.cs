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
using FSO.LotView.Model;
using FSO.SimAntics.NetPlay.Model;
using System.IO;

namespace FSO.SimAntics.Marshals.Hollow
{
    public class VMHollowGameObjectMarshal : VMSerializable
    {
        public short ObjectID;
        public uint GUID;
        public uint MasterGUID;
        public LotTilePos Position;
        public Direction Direction;
        public short Graphic;
        public ulong DynamicSpriteFlags;
        public ulong DynamicSpriteFlags2;

        public short[] Contained; //object ids
        public short Container;
        public short ContainerSlot;

        public short Flags;
        public short Flags2;
        public short WallPlacementFlags;
        public short PlacementFlags;
        public short AllowedHeightFlags;

        public int Version;

        public VMHollowGameObjectMarshal() { }
        public VMHollowGameObjectMarshal(int version) { Version = version; }

        public void Deserialize(BinaryReader reader)
        {
            ObjectID = reader.ReadInt16();
            GUID = reader.ReadUInt32();
            MasterGUID = reader.ReadUInt32();
            Position = new LotTilePos();
            Position.Deserialize(reader);
            Direction = (Direction)reader.ReadByte();
            Graphic = reader.ReadInt16();
            DynamicSpriteFlags = reader.ReadUInt64();
            DynamicSpriteFlags2 = reader.ReadUInt64();

            var contC = reader.ReadInt32();
            Contained = new short[contC];
            for (int i=0; i<contC; i++)
            {
                Contained[i] = reader.ReadInt16();
            }
            Container = reader.ReadInt16();
            ContainerSlot = reader.ReadInt16();

            Flags = reader.ReadInt16();
            Flags2 = reader.ReadInt16();
            WallPlacementFlags = reader.ReadInt16();
            PlacementFlags = reader.ReadInt16();
            AllowedHeightFlags = reader.ReadInt16();
        }
        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(ObjectID);
            writer.Write(GUID);
            writer.Write(MasterGUID);
            Position.SerializeInto(writer);
            writer.Write((byte)Direction);
            writer.Write(Graphic);
            writer.Write(DynamicSpriteFlags);
            writer.Write(DynamicSpriteFlags2);

            writer.Write(Contained.Length);
            writer.Write(VMSerializableUtils.ToByteArray(Contained));
            writer.Write(Container);
            writer.Write(ContainerSlot);

            writer.Write(Flags);
            writer.Write(Flags2);
            writer.Write(WallPlacementFlags);
            writer.Write(PlacementFlags);
            writer.Write(AllowedHeightFlags);
        }
    }
}
