
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
    public class VMNetUpdatePersistStateCmd : VMNetCommandBodyAbstract
    {
        public short ObjectID;
        public uint PersistID;

        public override bool AcceptFromClient { get { return false; } }

        public override bool Execute(VM vm)
        {
            VMEntity obj = vm.GetObjectById(ObjectID);
            if (obj == null || (obj is VMAvatar)) return false;
            if (obj.PersistID > 0) vm.Context.ObjectQueries.RemoveMultitilePersist(vm, obj.PersistID); //in case persist is reassigned somehow
            foreach (var e in obj.MultitileGroup.Objects)
                e.PersistID = PersistID;
            vm.Context.ObjectQueries.RegisterMultitilePersist(obj.MultitileGroup, obj.PersistID);
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
            writer.Write(ObjectID);
            writer.Write(PersistID);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            ObjectID = reader.ReadInt16();
            PersistID = reader.ReadUInt32();
        }

        #endregion
    }
}
