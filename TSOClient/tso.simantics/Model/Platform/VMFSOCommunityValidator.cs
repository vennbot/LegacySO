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
using FSO.SimAntics.Model.TSOPlatform;

namespace FSO.SimAntics.Model.Platform
{
    public class VMFSOCommunityValidator : VMDefaultValidator
    {
        public VMFSOCommunityValidator(VM vm) : base(vm)
        {
        }

        public override bool CanMoveObject(VMAvatar ava, VMEntity obj)
        {
            return ava.AvatarState.Permissions >= VMTSOAvatarPermissions.Roommate;
        }

        public override bool CanSendbackObject(VMAvatar ava, VMGameObject obj)
        {
            return (obj.TSOState as VMTSOObjectState).OwnerID == ava.PersistID &&
                !(obj.TSOState as VMTSOObjectState).ObjectFlags.HasFlag(VMTSOObjectFlags.FSODonated);
        }

        public override DeleteMode GetDeleteMode(DeleteMode desired, VMAvatar ava, VMEntity obj)
        {
            if (desired > DeleteMode.Delete) return DeleteMode.Disallowed;
            if (obj == null || ava == null) return DeleteMode.Disallowed;
            if (desired == DeleteMode.Sendback && 
                (obj.PersistID == 0 || obj is VMAvatar || !CanSendbackObject(ava, (VMGameObject)obj))) return DeleteMode.Disallowed;

            if (obj is VMAvatar && ava.AvatarState.Permissions < VMTSOAvatarPermissions.Admin) return DeleteMode.Disallowed;

            if (ava.AvatarState.Permissions >= VMTSOAvatarPermissions.Owner)
                return desired;

            if (ava.AvatarState.Permissions < VMTSOAvatarPermissions.BuildBuyRoommate)
                return DeleteMode.Disallowed;

            //build buy donator can delete build mode objects that are donated

            if (!(obj.TSOState as VMTSOObjectState).ObjectFlags.HasFlag(VMTSOObjectFlags.FSODonated)) return DeleteMode.Disallowed;
            var catalog = Content.Content.Get().WorldCatalog;
            var item = catalog.GetItemByGUID(obj.Object.OBJ.GUID);

            if (item != null)
            {
                if (BuilderWhiteList.Contains(item.Value.Category) && !RoomieWhiteList.Contains(item.Value.Category))
                {
                    return desired;
                }
            }
            return DeleteMode.Disallowed;
        }

        public override PurchaseMode GetPurchaseMode(PurchaseMode desired, VMAvatar ava, uint guid, bool fromInventory)
        {
            if (desired > PurchaseMode.Donate) return PurchaseMode.Disallowed;
            if (ava == null) return PurchaseMode.Disallowed;
            if (ava.AvatarState.Permissions < TSOPlatform.VMTSOAvatarPermissions.Roommate) return PurchaseMode.Disallowed;
            if (base.GetPurchaseMode(desired, ava, guid, fromInventory) == PurchaseMode.Disallowed) return PurchaseMode.Disallowed;
            if (desired == PurchaseMode.Normal && ava.AvatarState.Permissions < TSOPlatform.VMTSOAvatarPermissions.Owner) return PurchaseMode.Donate;
            return desired;
        }

        public override bool CanBuildTool(VMAvatar ava)
        {
            return ava.AvatarState.Permissions >= VMTSOAvatarPermissions.BuildBuyRoommate;
        }

        public override bool CanManageAsyncSale(VMAvatar ava, VMGameObject obj)
        {
            //currently cannot sell anything on a community lot.
            //commented lets people sell non-donated objects
            return false; // CanSendbackObject(ava, obj);
        }

        public override bool CanManageAdmitList(VMAvatar ava)
        {
            return ava.AvatarState.Permissions >= VMTSOAvatarPermissions.Owner;
        }

        public override bool CanManageEnvironment(VMAvatar ava)
        {
            return ava.AvatarState.Permissions >= VMTSOAvatarPermissions.Owner;
        }

        public override bool CanManageLotSize(VMAvatar ava)
        {
            return ava.AvatarState.Permissions >= VMTSOAvatarPermissions.Admin;
        }

        public override bool CanChangePermissions(VMAvatar ava)
        {
            //permissions changes are handled differently on community lots.
            //the normal permissions changes should not function.
            return ava.AvatarState.Permissions >= VMTSOAvatarPermissions.Admin; 
        }
    }
}
