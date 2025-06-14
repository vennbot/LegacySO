
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
using System.Collections.Generic;
using System.IO;

namespace FSO.SimAntics.NetPlay.EODs.Handlers.Data
{
    public class VMEODGameCompDrawACardData : VMSerializable
    {
        public string GameTitle;
        public string GameDescription;
        public byte LastIndex = 0;
        public List<String> CardText;
        public List<byte> EachCardsCount;

        public VMEODGameCompDrawACardData() { }

        public VMEODGameCompDrawACardData(byte[] data)
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
            try
            {
                GameTitle = reader.ReadString();
                GameDescription = reader.ReadString();
                LastIndex = reader.ReadByte();
                this.CardText = new List<string>();
                EachCardsCount = new List<byte>();

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    string CardText = reader.ReadString();
                    this.CardText.Add(CardText);
                    EachCardsCount.Add(reader.ReadByte());
                }
            }
            catch(Exception)
            {

            }
        }

        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(GameTitle);
            writer.Write(GameDescription);
            writer.Write(LastIndex);

            for (int index = 0; index < EachCardsCount.Count; index++)
            {
                writer.Write(CardText[index]);
                writer.Write(EachCardsCount[index]);
            }
        }

        public static byte[] SerializeStrings(params string[] strings)
        {
            byte[] returnArray;
            if (strings == null) return null;
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                foreach (string rawString in strings)
                {
                    writer.Write(rawString);
                }
                returnArray = stream.ToArray();
                stream.Dispose();
                writer.Dispose();
                return returnArray;
            }
        }

        public static string[] DeserializeStrings(byte[] byteArray)
        {
            if (byteArray == null) return null;
            List<String> stringList = new List<string>();

            string deserializedString;
            using (var reader = new BinaryReader(new MemoryStream(byteArray)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    deserializedString = reader.ReadString();
                    if (deserializedString != null)
                        stringList.Add(deserializedString);
                }
                reader.BaseStream.Dispose();
                reader.Dispose();
            }

            return stringList.ToArray();
        }
    }
}
