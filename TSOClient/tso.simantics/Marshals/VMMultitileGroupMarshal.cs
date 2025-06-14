
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
using FSO.SimAntics.NetPlay.Model;
using System.IO;
using FSO.LotView.Model;

namespace FSO.SimAntics.Marshals
{
    public class VMMultitileGroupMarshal : VMSerializable
    {
        public bool MultiTile;
        public string Name;
        public int Price;
        public int SalePrice = -1;
        public short[] Objects;
        public LotTilePos[] Offsets;
        public int Version;

        public VMMultitileGroupMarshal() { }
        public VMMultitileGroupMarshal(int version) { Version = version; }

        public void Deserialize(BinaryReader reader)
        {
            MultiTile = reader.ReadBoolean();
            Name = reader.ReadString();
            Price = reader.ReadInt32();
            if (Version > 12) SalePrice = reader.ReadInt32();

            var objs = reader.ReadInt32();
            Objects = new short[objs];
            for (int i=0; i<objs; i++) Objects[i] = reader.ReadInt16();

            Offsets = new LotTilePos[objs];
            for (int i = 0; i < objs; i++)
            {
                Offsets[i] = new LotTilePos();
                Offsets[i].Deserialize(reader);
            }
        }

        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(MultiTile);
            writer.Write(Name);
            writer.Write(Price);
            writer.Write(SalePrice);
            writer.Write(Objects.Length);
            writer.Write(VMSerializableUtils.ToByteArray(Objects));
            foreach (var item in Offsets) item.SerializeInto(writer);
        }
    }
}
