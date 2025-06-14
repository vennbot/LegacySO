
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

namespace FSO.SimAntics.NetPlay.EODs.Handlers.Data
{
    public class VMEODSignsData : VMSerializable
    {
        public ushort Flags;
        public string Text = "";

        public VMEODSignsData() { }

        public VMEODSignsData(byte[] data)
        {
            using (var reader = new BinaryReader(new MemoryStream(data)))
            {
                Deserialize(reader);
            }
        }

        public byte[] Save()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                SerializeInto(writer);
                return stream.ToArray();
            }
            
        }

        public void Deserialize(BinaryReader reader)
        {
            Flags = reader.ReadUInt16();
            Text = reader.ReadString();
        }

        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(Flags);
            writer.Write(Text);
        }
    }
}
