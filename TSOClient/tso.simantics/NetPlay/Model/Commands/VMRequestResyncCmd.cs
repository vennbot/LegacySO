
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
    public class VMRequestResyncCmd : VMNetCommandBodyAbstract
    {
        public uint Reason;
        public uint TickID;

        public override bool Execute(VM vm)
        {
#if VM_DESYNC_DEBUG

#endif
            return true;
        }

        #region VMSerializable Members
        public override void SerializeInto(BinaryWriter writer)
        {
            writer.Write(Reason);
            writer.Write(TickID);
        }

        public override void Deserialize(BinaryReader reader)
        {
            Reason = reader.ReadUInt32();
            TickID = reader.ReadUInt32();
        }
        #endregion
    }
}
