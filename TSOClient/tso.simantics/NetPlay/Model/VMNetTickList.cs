
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
    public class VMNetTickList : VMSerializable
    {
        public bool ImmediateMode = false;
        public List<VMNetTick> Ticks;

        #region VMSerializable Members

        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(ImmediateMode);
            if (Ticks == null) writer.Write(0);
            else
            {
                writer.Write(Ticks.Count);
                for (int i=0; i<Ticks.Count; i++)
                {
                    Ticks[i].SerializeInto(writer);
                }
            }
        }

        public void Deserialize(BinaryReader reader)
        {
            ImmediateMode = reader.ReadBoolean();
            Ticks = new List<VMNetTick>();
            int length = reader.ReadInt32();
            for (int i=0; i<length; i++)
            {
                var cmds = new VMNetTick();
                cmds.Deserialize(reader);
                Ticks.Add(cmds);
            }
        }

        #endregion
    }
}
