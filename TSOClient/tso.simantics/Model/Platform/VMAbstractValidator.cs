
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
namespace FSO.SimAntics.Model.Platform
{
    public abstract class VMAbstractValidator
    {
        protected VM vm;
        public VMAbstractValidator(VM vm)
        {
            this.vm = vm;
        }

        public abstract DeleteMode GetDeleteMode(DeleteMode desired, VMAvatar ava, VMEntity obj);
        public abstract PurchaseMode GetPurchaseMode(PurchaseMode desired, VMAvatar ava, uint guid, bool fromInventory);
        public abstract bool CanBuildTool(VMAvatar ava);
        public abstract bool CanMoveObject(VMAvatar ava, VMEntity obj);
        public abstract bool CanSendbackObject(VMAvatar ava, VMGameObject obj);
        public abstract bool CanManageAsyncSale(VMAvatar ava, VMGameObject obj);

        public abstract bool CanManageAdmitList(VMAvatar ava);
        public abstract bool CanManageEnvironment(VMAvatar ava);
        public abstract bool CanManageLotSize(VMAvatar ava);
        public abstract bool CanChangePermissions(VMAvatar ava);
    }

    public enum DeleteMode : byte
    {
        Disallowed = 0,
        Sendback,
        Delete
    }

    public enum PurchaseMode : byte
    {
        Disallowed = 0,
        Normal,
        Donate
    }
}
