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
using FSO.SimAntics.Engine.Scopes;
using System.Linq;
using System.IO;
using FSO.SimAntics.Model;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetSetOutfitCmd : VMNetCommandBodyAbstract
    {
        public uint UID;
        public VMPersonSuits Scope;
        public ulong Outfit;

        public override bool Execute(VM vm)
        {
            VMAvatar avatar = vm.Entities.FirstOrDefault(x => x.PersistID == UID) as VMAvatar;
            if (avatar == null) { return false; }

            switch (Scope)
            {
                case VMPersonSuits.DefaultDaywear:
                    avatar.DefaultSuits.Daywear = new VMOutfitReference(Outfit);
                    break;
                case VMPersonSuits.DefaultSleepwear:
                    avatar.DefaultSuits.Sleepwear = new VMOutfitReference(Outfit);
                    break;
                case VMPersonSuits.DefaultSwimwear:
                    avatar.DefaultSuits.Swimwear = new VMOutfitReference(Outfit);
                    break;
                case VMPersonSuits.DynamicDaywear:
                    avatar.DynamicSuits.Daywear = Outfit;
                    break;
                case VMPersonSuits.DynamicSleepwear:
                    avatar.DynamicSuits.Sleepwear = Outfit;
                    break;
                case VMPersonSuits.DynamicSwimwear:
                    avatar.DynamicSuits.Swimwear = Outfit;
                    break;
                case VMPersonSuits.DynamicCostume:
                    avatar.DynamicSuits.Costume = Outfit;
                    break;
                case VMPersonSuits.DecorationHead:
                    avatar.Decoration.Head = Outfit;
                    break;
                case VMPersonSuits.DecorationBack:
                    avatar.Decoration.Back = Outfit;
                    break;
                case VMPersonSuits.DecorationShoes:
                    avatar.Decoration.Shoes = Outfit;
                    break;
                case VMPersonSuits.DecorationTail:
                    avatar.Decoration.Tail = Outfit;
                    break;
            }

            return true;
        }

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(UID);
            writer.Write((short)Scope);
            writer.Write(Outfit);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            UID = reader.ReadUInt32();
            Scope = (VMPersonSuits)reader.ReadInt16();
            Outfit = reader.ReadUInt64();
        }
    }
}
