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

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetSM64AnimDataCmd : VMNetCommandBodyAbstract
    {
        public byte[] AnimData;

        public override bool AcceptFromClient { get { return false; } }

        public override bool Execute(VM vm, VMAvatar caller)
        {
            LotView.Components.SM64Component.SetAnimData(AnimData);

            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            return !FromNet;
        }

        #region VMSerializable Members

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);

            writer.Write(AnimData.Length);
            writer.Write(AnimData);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);

            int length = reader.ReadInt32();
            AnimData = reader.ReadBytes(length);
        }

        #endregion
    }
}
