
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
using System.Collections.Generic;
using System.IO;

namespace FSO.SimAntics.Model
{
    public class VMInventoryItem : VMSerializable
    {
        public uint ObjectPID;
        public string Name;
        public uint GUID;
        public ushort Graphic;
        public uint Value;
        public ulong DynFlags1;
        public ulong DynFlags2;

        public byte AttributeMode;
        public List<int> Attributes = new List<int>();

        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(ObjectPID);
            writer.Write(Name);
            writer.Write(GUID);
            writer.Write(Graphic);
            writer.Write(Value);
            writer.Write(DynFlags1);
            writer.Write(DynFlags2);

            writer.Write(AttributeMode);
            writer.Write(Attributes.Count);
            foreach (var attribute in Attributes)
            {
                writer.Write(attribute);
            }
        }

        public void Deserialize(BinaryReader reader)
        {
            ObjectPID = reader.ReadUInt32();
            Name = reader.ReadString();
            GUID = reader.ReadUInt32();
            Graphic = reader.ReadUInt16();
            Value = reader.ReadUInt32();
            DynFlags1 = reader.ReadUInt64();
            DynFlags2 = reader.ReadUInt64();

            AttributeMode = reader.ReadByte();
            var attrCount = reader.ReadInt32();
            for (int i=0; i<attrCount; i++)
            {
                Attributes.Add(reader.ReadInt32());
            }
        }
    }
}
