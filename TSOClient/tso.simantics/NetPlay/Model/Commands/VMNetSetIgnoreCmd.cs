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
using FSO.SimAntics.Model.TSOPlatform;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetSetIgnoreCmd : VMNetCommandBodyAbstract
    {
        public uint TargetPID;
        public bool SetIgnore;

        public override bool Execute(VM vm, VMAvatar caller)
        {
            if (caller == null) return false;
            var ignored = ((VMTSOAvatarState)caller.TSOState).IgnoredAvatars;
            if (SetIgnore && ignored.Count < 128) ignored.Add(TargetPID);
            else ignored.Remove(TargetPID);
            return true;
        }

        #region VMSerializable Members

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(TargetPID);
            writer.Write(SetIgnore);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            TargetPID = reader.ReadUInt32();
            SetIgnore = reader.ReadBoolean();
        }

        #endregion
    }
}
