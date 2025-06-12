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
using FSO.Files.Utils;
using System;
using System.IO;

namespace FSO.Files.Formats.tsodata
{
    public class BulletinItem
    {
        public static int CURRENT_VERSION = 1;
        public int Version = CURRENT_VERSION;
        public uint ID;
        public uint NhoodID;
        public uint SenderID;

        public string Subject;
        public string Body;
        public string SenderName;

        public long Time;

        public BulletinType Type;
        public BulletinFlags Flags;

        public uint LotID; //optional: for lot advertisements.

        public BulletinItem()
        {

        }

        public BulletinItem(Stream stream)
        {
            Read(stream);
        }

        public void Save(Stream stream)
        {
            using (var writer = IoWriter.FromStream(stream))
            {
                writer.WriteCString("FSOB", 4);
                writer.WriteInt32(Version);
                writer.WriteUInt32(ID);
                writer.WriteUInt32(NhoodID);
                writer.WriteUInt32(SenderID);

                writer.WriteLongPascalString(Subject);
                writer.WriteLongPascalString(Body);
                writer.WriteLongPascalString(SenderName);
                writer.WriteInt64(Time);
                writer.WriteInt32((int)Type);
                writer.WriteInt32((int)Flags);

                writer.WriteUInt32(LotID);
            }
        }

        public void Read(Stream stream)
        {
            using (var reader = IoBuffer.FromStream(stream))
            {
                var magic = reader.ReadCString(4);
                Version = reader.ReadInt32();
                ID = reader.ReadUInt32();
                NhoodID = reader.ReadUInt32();
                SenderID = reader.ReadUInt32();

                Subject = reader.ReadLongPascalString();
                Body = reader.ReadLongPascalString();
                SenderName = reader.ReadLongPascalString();
                Time = reader.ReadInt64();
                Type = (BulletinType)reader.ReadInt32();
                Flags = (BulletinFlags)reader.ReadInt32();

                LotID = reader.ReadUInt32();
            }
        }
    }

    public enum BulletinType
    {
        Mayor = 0,
        System = 1,
        Community = 2,
    }

    [Flags]
    public enum BulletinFlags
    {
        PromotedByMayor = 1
    }
}
