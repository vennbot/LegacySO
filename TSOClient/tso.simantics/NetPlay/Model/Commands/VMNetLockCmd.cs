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
    public class VMNetLockCmd : VMNetCommandBodyAbstract
    {
        public short ObjectID;

        public static void LockObj(VM vm, VMEntity obj)
        {
            vm.SendCommand(new VMNetLockCmd
            {
                ObjectID = obj.ObjectID
            });
            //lock IMMEDIATELY on server, so you can't sneak in multiple commands in sequence between the lock delay.
            //locks usually only checked in Verify stage so shouldn't cause desyncs on client.
            foreach (var o in obj.MultitileGroup.Objects) {
                if (o is VMGameObject) ((VMGameObject)o).Disabled |= VMGameObjectDisableFlags.TransactionIncomplete;
            }
                
        }

        public override bool Execute(VM vm)
        {
            VMEntity obj = vm.GetObjectById(ObjectID);
            if (obj == null) return false;
            foreach (var o in obj.MultitileGroup.Objects)
            {
                if (o is VMGameObject) ((VMGameObject)o).Disabled |= VMGameObjectDisableFlags.TransactionIncomplete;
            }
            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            return !FromNet;
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            ObjectID = reader.ReadInt16();
        }

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(ObjectID);
        }
    }
}
