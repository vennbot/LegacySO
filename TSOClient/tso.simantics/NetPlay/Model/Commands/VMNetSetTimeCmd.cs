
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
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetSetTimeCmd : VMNetCommandBodyAbstract
    {
        public int Hours;
        public int Minutes;
        public int Seconds;
        public long UTCStart;

        public override bool Execute(VM vm)
        {
            var clock = vm.Context.Clock;
            clock.Hours = Hours;
            clock.Minutes = Minutes;
            clock.MinuteFractions = (Seconds * clock.TicksPerMinute) / 60;
            clock.UTCStart = UTCStart;
            clock.Ticks = 0;
            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            return !FromNet;
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            Hours = reader.ReadInt32();
            Minutes = reader.ReadInt32();
            Seconds = reader.ReadInt32();
            UTCStart = reader.ReadInt64();
        }

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(Hours);
            writer.Write(Minutes);
            writer.Write(Seconds);
            writer.Write(UTCStart);
        }
    }
}
