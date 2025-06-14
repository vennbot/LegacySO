
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
using FSO.SimAntics.Primitives;
using System.IO;

namespace FSO.SimAntics.Marshals
{
    public class VMRuntimeHeadlineMarshal : VMSerializable
    {
        public VMSetBalloonHeadlineOperand Operand;
        public short Target;
        public short IconTarget;
        public sbyte Index;
        public int Duration;
        public int Anim;

        public void SerializeInto(BinaryWriter writer)
        {
            var op = new byte[8];
            Operand.Write(op);
            writer.Write(op);
            writer.Write(Target);
            writer.Write(IconTarget);
            writer.Write(Index);
            writer.Write(Duration);
            writer.Write(Anim);
        }

        public void Deserialize(BinaryReader reader)
        {
            Operand = new VMSetBalloonHeadlineOperand();
            Operand.Read(reader.ReadBytes(8));
            Target = reader.ReadInt16();
            IconTarget = reader.ReadInt16();
            Index = reader.ReadSByte();
            Duration = reader.ReadInt32();
            Anim = reader.ReadInt32();
        }
    }
}
