
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
using FSO.SimAntics.Model;
using System.Collections.Generic;
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetUpdateInventoryCmd : VMNetCommandBodyAbstract
    {
        public List<VMInventoryItem> Items;

        public override bool AcceptFromClient { get { return false; } }

        public override bool Execute(VM vm)
        {
            //sent direct to the target, so we should believe the inventory is ours.
            vm.MyInventory = Items;
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
            writer.Write(Items.Count);
            foreach (var item in Items)
            {
                item.SerializeInto(writer);
            }
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            var count = reader.ReadInt32();
            Items = new List<VMInventoryItem>();
            for (int i=0; i<count; i++)
            {
                var item = new VMInventoryItem();
                item.Deserialize(reader);
                Items.Add(item);
            }
        }

        #endregion
    }

}
