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
using FSO.LotView.Model;

namespace FSO.SimAntics.Marshals
{
    public class VMGameObjectMarshal : VMEntityMarshal
    {
        public Direction Direction;
        public VMGameObjectDisableFlags Disabled;

        public VMGameObjectMarshal() { }
        public VMGameObjectMarshal(int version, bool ts1) : base(version, ts1) { }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            Direction = (Direction)reader.ReadByte();
            if (Version > 9) Disabled = (VMGameObjectDisableFlags)reader.ReadByte();
        }
        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write((byte)Direction);
            writer.Write((byte)Disabled);
        }
    }
}
