
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
using FSO.SimAntics.Model.Platform;
using System.IO;
using FSO.SimAntics.Model.TSOPlatform;

namespace FSO.SimAntics.Model.TS1Platform
{
    public class VMTS1AvatarState : VMTS1EntityState, VMIAvatarState
    {
        public VMTS1AvatarState() { }
        public VMTS1AvatarState(int version) : base(version) { }

        public VMTSOAvatarPermissions Permissions
        {
            get; set;
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            Permissions = (VMTSOAvatarPermissions)reader.ReadByte();
        }

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write((byte)Permissions);
        }

        public override void Tick(VM vm, object owner)
        {
            base.Tick(vm, owner);
        }
    }
}
