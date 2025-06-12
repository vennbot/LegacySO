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
using FSO.SimAntics.Model.Platform;
using System.IO;

namespace FSO.SimAntics.Model.TS1Platform
{
    public class VMTS1ObjectState : VMTS1EntityState, VMIObjectState
    {
        public ushort Wear
        {
            get; set;
        }

        public VMTS1ObjectState() { }
        public VMTS1ObjectState(int version) : base(version) { }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            Wear = reader.ReadUInt16();
        }

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(Wear);
        }

        public override void Tick(VM vm, object owner)
        {
            base.Tick(vm, owner);
        }

        public void ProcessQTRDay(VM vm, VMEntity owner)
        {
            
        }
    }
}
