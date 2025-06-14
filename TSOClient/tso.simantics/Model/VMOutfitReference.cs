
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
using System;
using System.IO;
using FSO.Vitaboy;
using FSO.Files.Formats.IFF.Chunks;

namespace FSO.SimAntics.Model
{
    public class VMOutfitReference : VMSerializable
    {
        public string Name = "";
        public ulong ID = 0;
        public Outfit OftData;

        public VMOutfitReference(string name)
        {
            ID = uint.MaxValue;
            Name = name;
        }

        public VMOutfitReference(ulong id)
        {
            ID = id;
        }

        public VMOutfitReference(Outfit oft)
        {
            OftData = oft;
        }

        public VMOutfitReference(STR str, bool head)
        {
            OftData = new Outfit();
            if (head)
                OftData.ReadHead(str.GetString(2));
            else 
                OftData.Read(str);
        }

        public VMOutfitReference(string data, bool head)
        {
            OftData = new Outfit();
            if (head)
                OftData.ReadHead(data);
            else
                OftData.Read(data);
        }

        public static VMOutfitReference Parse(string data, bool ts1)
        {
            ts1 = false;
            if (ts1)
            {
                return new VMOutfitReference(data.Trim());
            } else
            {
                return new VMOutfitReference(Convert.ToUInt64(data.Trim(), 16));
            }
        }

        public Outfit GetContent()
        {
            if (OftData != null) return OftData;
            var content = Content.Content.Get().AvatarOutfits;
            if (ID == uint.MaxValue)
                return content.Get(Name);
            else
                return content.Get(ID);
        }

        public VMOutfitReference(BinaryReader reader)
        {
            Deserialize(reader);
        }

        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(ID);
            if (ID == uint.MaxValue) writer.Write(Name);
        }

        public void Deserialize(BinaryReader reader)
        {
            ID = reader.ReadUInt64();
            if (ID == uint.MaxValue) Name = reader.ReadString();
        }
    }
}
