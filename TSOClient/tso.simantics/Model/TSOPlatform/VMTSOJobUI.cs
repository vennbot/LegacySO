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
using FSO.SimAntics.NetPlay.Model;
using System.Collections.Generic;
using System.IO;

namespace FSO.SimAntics.Model.TSOPlatform
{
    public class VMTSOJobUI : VMSerializable
    {
        public List<string> MessageText = new List<string>();
        public int Minutes;
        public int Seconds;
        public VMTSOJobMode Mode;

        public void Deserialize(BinaryReader reader)
        {
            var count = reader.ReadInt32();
            for (int i=0; i<count; i++)
            {
                MessageText.Add(reader.ReadString());
            }
            Minutes = reader.ReadByte();
            Seconds = reader.ReadByte();
            Mode = (VMTSOJobMode)reader.ReadByte();

        }

        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(MessageText.Count);
            foreach (var msg in MessageText)
            {
                writer.Write(msg);
            }
            writer.Write((byte)Minutes);
            writer.Write((byte)Seconds);
            writer.Write((byte)Mode);
        }
    }

    public enum VMTSOJobMode
    {
        BeforeWork = 0,
        AfterWork = 1,
        Intermission = 2,
        Round = 3
    }
}
