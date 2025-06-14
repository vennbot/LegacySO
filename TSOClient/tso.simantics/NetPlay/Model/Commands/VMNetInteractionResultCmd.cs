
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
using System.Linq;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetInteractionResultCmd : VMNetCommandBodyAbstract
    {
        public ushort ActionUID;
        public bool Accepted;
        public override bool Execute(VM vm, VMAvatar caller)
        {
            if (caller == null) return false;
            var interaction = caller.Thread.Queue.FirstOrDefault(x => x.UID == ActionUID);
            if (interaction != null)
            {
                interaction.InteractionResult = (sbyte)(Accepted ? 2 : 1);
            }
            return true;
        }

        #region VMSerializable Members

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(ActionUID);
            writer.Write(Accepted);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            ActionUID = reader.ReadUInt16();
            Accepted = reader.ReadBoolean();
        }

        #endregion
    }
}
