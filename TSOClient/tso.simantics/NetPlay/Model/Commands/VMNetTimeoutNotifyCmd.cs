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
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetTimeoutNotifyCmd : VMNetCommandBodyAbstract
    {
        public int TimeRemaining; //in 60th seconds

        public override bool Execute(VM vm)
        {
            //sent from a server. we should be a VM.
            vm.SignalGenericVMEvt(VMEventType.TSOTimeout, TimeRemaining);
            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            //the fact the client executed this command should stop them from being afk.
            return false;
        }

        #region VMSerializable Members

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(TimeRemaining);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            TimeRemaining = reader.ReadInt32();
        }

        #endregion
    }
}
