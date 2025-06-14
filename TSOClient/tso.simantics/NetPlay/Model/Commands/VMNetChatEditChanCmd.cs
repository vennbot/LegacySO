
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
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetChatEditChanCmd : VMNetCommandBodyAbstract
    {
        public VMTSOChatChannel Channel;

        public override bool Execute(VM vm, VMAvatar caller)
        {
            vm.TSOState.ChatChannels.RemoveAll(x => x.ID == Channel.ID);
            if ((Channel.Flags & VMTSOChatChannelFlags.Delete) > 0) return true;

            //sanitize a few things
            Channel.Flags &= VMTSOChatChannelFlags.All;
            Channel.TextColor.A = 255;
            if (Channel.Name.Length > 8) Channel.Name = Channel.Name.Substring(0, 8);
            if (Channel.Description.Length > 256) Channel.Description = Channel.Description.Substring(0, 256);

            vm.TSOState.ChatChannels.Add(Channel);
            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            if (caller.AvatarState.Permissions < VMTSOAvatarPermissions.Owner) return false;
            if (Channel.ID == 0 || Channel.ID > 4) return false; //only user editable channels are 1-4.
            if (Channel.SendPermMin > VMTSOAvatarPermissions.Owner || Channel.ViewPermMin > VMTSOAvatarPermissions.Owner) return false;
            return true;
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            Channel = new VMTSOChatChannel();
            Channel.Deserialize(reader);
        }

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            Channel.SerializeInto(writer);
        }
    }
}
