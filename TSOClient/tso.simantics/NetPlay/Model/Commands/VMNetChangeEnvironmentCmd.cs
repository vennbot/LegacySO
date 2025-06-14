
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
using System;
using System.Collections.Generic;
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetChangeEnvironmentCmd : VMNetCommandBodyAbstract
    {
        public List<uint> GUIDsToAdd;
        public List<uint> GUIDsToClear;

        public override bool Execute(VM vm)
        {
            var amb = vm.Context.Ambience;
            foreach (var guid in GUIDsToClear)
                amb.SetAmbience(amb.GetAmbienceFromGUID(guid), false);
            foreach (var guid in GUIDsToAdd)
                amb.SetAmbience(amb.GetAmbienceFromGUID(guid), true);
            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            if (caller == null || //caller must be on lot, be a build roommate.
            caller.AvatarState.Permissions < VMTSOAvatarPermissions.BuildBuyRoommate)
                return false;
            return true;
        }

        #region VMSerializable Members

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(GUIDsToAdd.Count);
            foreach (var guid in GUIDsToAdd) writer.Write(guid);
            writer.Write(GUIDsToClear.Count);
            foreach (var guid in GUIDsToClear) writer.Write(guid);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);

            GUIDsToAdd = new List<uint>();
            var totalAdd = Math.Min(40, reader.ReadInt32());
            for (int i = 0; i < totalAdd; i++) GUIDsToAdd.Add(reader.ReadUInt32());

            GUIDsToClear = new List<uint>();
            var totalClear = Math.Min(40, reader.ReadInt32());
            for (int i = 0; i < totalClear; i++) GUIDsToClear.Add(reader.ReadUInt32());
        }

        #endregion
    }
}
