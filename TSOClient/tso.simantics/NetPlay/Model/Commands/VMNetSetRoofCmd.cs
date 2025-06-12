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
using System;
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetSetRoofCmd : VMNetCommandBodyAbstract
    {
        public float Pitch;
        public uint Style;

        public override bool Execute(VM vm, VMAvatar caller)
        {
            if (!vm.TS1 && (caller == null || caller.AvatarState.Permissions < VMTSOAvatarPermissions.Owner)) return false;
            if (Style >= Content.Content.Get().WorldRoofs.Count) return false;
            Pitch = Math.Max(0f, Math.Min(1.25f, Pitch));
            vm.Context.Architecture.SetRoof(Pitch, Style);
            return true;
        }

        #region VMSerializable Members

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(Pitch);
            writer.Write(Style);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            Pitch = reader.ReadSingle();
            Style = reader.ReadUInt32();
        }

        #endregion
    }
}
