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
using FSO.SimAntics.Model;
using System.Collections.Generic;
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetAsyncPriceCmd : VMNetCommandBodyAbstract
    {
        public uint ObjectPID;
        public int NewPrice;

        public override bool Execute(VM vm, VMAvatar caller)
        {
            VMEntity obj = vm.GetObjectByPersist(ObjectPID);
            //object must not be in use to set it for sale (will be disabled).
            if (!vm.PlatformState.Validator.CanManageAsyncSale(caller, (VMGameObject)obj)) return false;

            if (NewPrice > 0 && obj.IsUserMovable(vm.Context, true) != VMPlacementError.Success) return false;
            if ((((VMGameObject)obj).Disabled & VMGameObjectDisableFlags.TransactionIncomplete) > 0) return false; //can't change price mid trasaction...
            //must own the object to set it for sale
            

            if (NewPrice >= 0) {
                //get catalog item for the object
                var item = Content.Content.Get().WorldCatalog.GetItemByGUID((obj.MasterDefinition ?? obj.Object.OBJ).GUID);
                if (item != null && item.Value.DisableLevel > 1) return false;
                foreach (var o in obj.MultitileGroup.Objects) ((VMGameObject)o).Disabled |= VMGameObjectDisableFlags.ForSale;
                obj.MultitileGroup.SalePrice = NewPrice;
            }
            else
            {
                foreach (var o in obj.MultitileGroup.Objects) ((VMGameObject)o).Disabled &= ~VMGameObjectDisableFlags.ForSale;
                obj.MultitileGroup.SalePrice = -1;
            }
            vm.Context.RefreshLighting(vm.Context.GetObjectRoom(obj), true, new HashSet<ushort>());
            return true;
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            ObjectPID = reader.ReadUInt32();
            NewPrice = reader.ReadInt32();
        }

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(ObjectPID);
            writer.Write(NewPrice);
        }
    }
}
