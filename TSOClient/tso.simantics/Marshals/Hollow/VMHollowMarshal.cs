
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
using System.IO.Compression;

namespace FSO.SimAntics.Marshals.Hollow
{
    public class VMHollowMarshal : VMSerializable
    {
        public int Version = VMMarshal.LATEST_VERSION;
        public bool Compressed = true;
        public VMContextMarshal Context;

        public VMHollowGameObjectMarshal[] Entities;
        public VMMultitileGroupMarshal[] MultitileGroups;

        public void Deserialize(BinaryReader reader)
        {
            if (new string(reader.ReadChars(4)) != "FSOh") return;

            Version = reader.ReadInt32();
            Compressed = reader.ReadBoolean();

            MemoryStream cStream;
            GZipStream zipStream;
            if (Compressed)
            {
                var length = reader.ReadInt32();
                cStream = new MemoryStream(reader.ReadBytes(length));
                zipStream = new GZipStream(cStream, CompressionMode.Decompress);
                reader = new BinaryReader(zipStream);
            }

            Context = new VMContextMarshal(Version);
            Context.Deserialize(reader);

            int ents = reader.ReadInt32();
            Entities = new VMHollowGameObjectMarshal[ents];
            for (int i = 0; i < ents; i++)
            {
                var ent = new VMHollowGameObjectMarshal(Version);
                ent.Deserialize(reader);
                Entities[i] = ent;
            }

            int mtgN = reader.ReadInt32();
            MultitileGroups = new VMMultitileGroupMarshal[mtgN];
            for (int i = 0; i < mtgN; i++)
            {
                MultitileGroups[i] = new VMMultitileGroupMarshal(Version);
                MultitileGroups[i].Deserialize(reader);
            }
        }

        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(new char[] { 'F', 'S', 'O', 'h' });
            writer.Write(VMMarshal.LATEST_VERSION);
            writer.Write(Compressed);

            var uWriter = writer;
            MemoryStream cStream = null;
            GZipStream zipStream = null;
            if (Compressed)
            {
                cStream = new MemoryStream();
                zipStream = new GZipStream(cStream, CompressionMode.Compress);
                writer = new BinaryWriter(zipStream);
            }

            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            Context.SerializeInto(writer);

            writer.Write(Entities.Length);
            foreach (var ent in Entities)
            {
                ent.SerializeInto(writer);
            }

            writer.Write(MultitileGroups.Length);
            foreach (var grp in MultitileGroups) grp.SerializeInto(writer);

            if (Compressed)
            {
                writer.Close();
                zipStream.Close();
                var data = cStream.ToArray();
                uWriter.Write(data.Length);
                uWriter.Write(data);
            }
        }
    }
}
