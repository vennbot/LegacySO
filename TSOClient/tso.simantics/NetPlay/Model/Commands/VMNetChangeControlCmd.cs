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
    public class VMNetChangeControlCmd : VMNetCommandBodyAbstract
    {
        public short TargetID;

        public override bool Execute(VM vm, VMAvatar caller)
        {
            //switch this client's controlled sim to another
            //only used for TS1.

            //not suitable for networking right now. If you want to do anything weird with ts1 multiplayer, needs these additions:
            // - block stealing sims controlled by others
            // - block assuming control of npcs (this is enforced by simitone ui)
            // - multiplayer handling of "selected sim" global

            var target = vm.GetObjectById(TargetID);
            if (target == null || target is VMGameObject) return false;

            if (caller != null && caller.PersistID == ActorUID)
            {
                //relinquish previous control
                vm.Context.ObjectQueries.RemoveAvatarPersist(caller.PersistID);
                caller.PersistID = 0;
            }

            target.PersistID = ActorUID;
            vm.Context.ObjectQueries.RegisterAvatarPersist((VMAvatar)target, target.PersistID);
            vm.SetGlobalValue(3, target.ObjectID);

            if (VM.UseWorld)
            {
                vm.Context.World.CenterTo(target.WorldUI);
            }

            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            return vm.TS1;
        }

        #region VMSerializable Members

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(TargetID);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            TargetID = reader.ReadInt16();
        }

        #endregion
    }
}
