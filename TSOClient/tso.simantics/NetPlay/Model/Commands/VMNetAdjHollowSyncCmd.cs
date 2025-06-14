
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
    public class VMNetAdjHollowSyncCmd : VMNetCommandBodyAbstract
    {
        public byte[][] HollowAdj;

        public override bool AcceptFromClient { get { return false; } }

        public override bool Execute(VM vm)
        {
            vm.HollowAdj = HollowAdj;
            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            return !FromNet;
        }

        #region VMSerializable Members

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(HollowAdj.Length);
            foreach (var item in HollowAdj)
            {
                if (item == null) writer.Write(false);
                else
                {
                    writer.Write(true);
                    writer.Write(item.Length);
                    writer.Write(item);
                }
            }
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            HollowAdj = new byte[reader.ReadInt32()][];
            for (int i=0; i<HollowAdj.Length; i++)
            {
                if (reader.ReadBoolean()) HollowAdj[i] = reader.ReadBytes(reader.ReadInt32());
            }
        }

        #endregion
    }
}
