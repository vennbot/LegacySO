
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

namespace FSO.SimAntics.NetPlay.EODs.Model
{
    public class VMEODEvent : VMSerializable
    {
        public short Code;
        public short[] Data;

        public VMEODEvent() { }
        public VMEODEvent(short code)
        {
            Code = code;
            Data = new short[0];
        }
        public VMEODEvent(short code, params short[] data) {
            Code = code;
            Data = data;
        }

        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(Code);
            writer.Write((byte)Data.Length);
            foreach (var dat in Data) writer.Write(dat);
        }

        public void Deserialize(BinaryReader reader)
        {
            Code = reader.ReadInt16();
            var length = Math.Min((byte)4, reader.ReadByte());
            Data = new short[length];
            for (int i = 0; i < length; i++)
                Data[i] = reader.ReadInt16();
        }
    }
}
