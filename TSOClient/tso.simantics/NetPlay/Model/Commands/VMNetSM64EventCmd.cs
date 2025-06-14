
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
using FSO.LotView.Components;
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    internal class VMNetSM64EventCmd : VMNetCommandBodyAbstract
    {
        public int EventType;
        public uint EventValue;

        public override bool Execute(VM vm, VMAvatar caller)
        {
            // Tell the SM64 component about this sim's mario instance.
            if (caller == null || caller.WorldUI == null || !(caller.WorldUI is AvatarComponent)) return false;

            if (caller.PersistID != vm.MyUID)
            {
                vm.Context.Blueprint.SM64?.PlaySound((AvatarComponent)caller.WorldUI, EventValue);
            }

            return true;
        }

        #region VMSerializable Members

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);

            writer.Write(EventType);
            writer.Write(EventValue);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);

            EventType = reader.ReadInt32();
            EventValue = reader.ReadUInt32();
        }

        #endregion
    }
}
