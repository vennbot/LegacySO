
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
using System.Collections.Generic;
using System.IO;

namespace FSO.SimAntics.NetPlay.Model
{
    public class VMNetTick : VMSerializable
    {
        public uint TickID;
        public ulong RandomSeed;
        public bool ImmediateMode; //not serialized

        public List<VMNetCommand> Commands;

        #region VMSerializable Members

        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(TickID);
            writer.Write(RandomSeed);

            if (Commands == null) writer.Write(0);
            else
            {
                writer.Write(Commands.Count);
                for (int i=0; i<Commands.Count; i++)
                {
                    var cmd = Commands[i];
                    cmd.SerializeInto(writer);
                }
            }
        }

        public void Deserialize(BinaryReader reader)
        {
            TickID = reader.ReadUInt32();
            RandomSeed = reader.ReadUInt64();

            Commands = new List<VMNetCommand>();
            int length = reader.ReadInt32();
            for (int i=0; i<length; i++)
            {
                var cmd = new VMNetCommand();
                cmd.Deserialize(reader);
                Commands.Add(cmd);
            }
        }

        #endregion
    }
}
