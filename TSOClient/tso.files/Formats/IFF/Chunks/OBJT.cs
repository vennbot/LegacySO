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
using System.Collections.Generic;
using System.IO;

namespace FSO.Files.Formats.IFF.Chunks
{
    /**
     * Object type information. Contains all object types used on the lot,
     * along with some version information that resets objects of a type if it differs.
     */
    public class OBJT : IffChunk
    {
        public List<OBJTEntry> Entries;

        public override void Read(IffFile iff, Stream stream)
        {
            using (var io = IoBuffer.FromStream(stream, ByteOrder.LITTLE_ENDIAN))
            {
                var zero = io.ReadInt32();
                var version = io.ReadInt32(); //should be 2/3
                var objt = io.ReadInt32(); //tjbo

                Entries = new List<OBJTEntry>();
                //single tile objects are named. multitile objects arent.

                while (io.HasMore)
                {
                    Entries.Add(new OBJTEntry(io, version));
                }
            }
        }
    }

    public class OBJTEntry
    {
        public uint GUID;
        public ushort Unknown1a;
        public ushort InitTreeVersion;
        public ushort Unknown2a;
        public ushort MainTreeVersion;
        public ushort TypeID;
        public OBJDType OBJDType;
        public string Name;
        public OBJTEntry(IoBuffer io, int version)
        {
            //16 bytes of data
            GUID = io.ReadUInt32();
            if (GUID == 0) return;
            Unknown1a = io.ReadUInt16(); //likely number of attributes
            InitTreeVersion = io.ReadUInt16();
            Unknown2a = io.ReadUInt16(); //objd version?
            MainTreeVersion = io.ReadUInt16();
            //increases by one each time, one based, essentially an ID for this loaded type. Mostly matches index in array, but I guess it can possibly be different.
            TypeID = io.ReadUInt16(); 
            OBJDType = (OBJDType)io.ReadUInt16();
            //then the name, null terminated
            Name = io.ReadNullTerminatedString();
            if (Name.Length%2 == 0) io.ReadByte(); //pad to short width
            if (version > 2) io.ReadInt32(); //not sure what this is
        }

        public override string ToString()
        {
            return $"{TypeID}: {Name} ({GUID.ToString("x8")}): [{Unknown1a}, {InitTreeVersion}, {Unknown2a}, {MainTreeVersion}, {OBJDType}]";
        }
    }
}
