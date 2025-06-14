
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
using FSO.SimAntics.Model.TSOPlatform;
using System;
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetChangeLotSizeCmd : VMNetCommandBodyAbstract
    {
        public byte LotSize;
        public byte LotStories;
        //lot direction must be changed from city.

        private bool Verified;
        public override bool Execute(VM vm)
        {
            vm.TSOState.Size &= 0xFF0000;
            vm.TSOState.Size |= (LotSize) | (LotStories << 8);
            vm.Context.UpdateTSOBuildableArea();
            vm.Context.Architecture.SignalAllDirty();
            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            if (Verified) return true; //set internally when transaction succeeds. trust that the verification happened.

            if (caller == null || //caller must be on lot, have owner permissions
                caller.AvatarState.Permissions < VMTSOAvatarPermissions.Owner)
                return false;

            var exState = vm.TSOState.Size;
            var exSize = exState & 255;
            var exStories = (exState >> 8) & 255;

            LotSize = Math.Min(LotSize, (byte)(VMBuildableAreaInfo.BuildableSizes.Length - 1));
            LotStories = Math.Min(LotStories, (byte)3);

            if ((exSize == LotSize && exStories == LotStories) || LotSize < exSize || LotStories < exStories)
            {
                //cannot size down. cannot stay same size (no-op)
                return false;
            } else
            {
                var totalOld = exSize + exStories;
                var totalTarget = LotSize + LotStories;
                var baseCost = VMBuildableAreaInfo.CalculateBaseCost(totalOld, totalTarget);
                var roomieCost = VMBuildableAreaInfo.CalculateRoomieCost(vm.TSOState.Roommates.Count, totalOld, totalTarget);

                //perform the transaction. If it succeeds, requeue the command
                vm.GlobalLink.PerformTransaction(vm, false, caller.PersistID, uint.MaxValue, baseCost+roomieCost,
                    (bool success, int transferAmount, uint uid1, uint budget1, uint uid2, uint budget2) =>
                    {
                        if (success)
                        {
                            Verified = true;
                            vm.ForwardCommand(this);
                        }
                    });
                return false;
            }
        }

        #region VMSerializable Members

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(LotSize);
            writer.Write(LotStories);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            LotSize = reader.ReadByte();
            LotStories = reader.ReadByte();
        }

        #endregion
    }
}
