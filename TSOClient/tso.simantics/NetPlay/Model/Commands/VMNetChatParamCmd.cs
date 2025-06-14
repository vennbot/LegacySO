
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
using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetChatParamCmd : VMNetCommandBodyAbstract
    {
        public sbyte Pitch;
        public Color Col;

        public override bool Execute(VM vm, VMAvatar caller)
        {
            var state = (VMTSOAvatarState)caller.TSOState;
            Col.A = 255;

            state.ChatTTSPitch = Math.Max((sbyte)-100, Math.Min((sbyte)100, Pitch));
            state.ChatColor = Col;
            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            return caller != null;
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            Pitch = reader.ReadSByte();
            Col = new Color(reader.ReadUInt32());
        }

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(Pitch);
            writer.Write(Col.PackedValue);
        }
    }
}
