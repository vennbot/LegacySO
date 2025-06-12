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
using FSO.Files.Formats.IFF.Chunks;
using FSO.SimAntics.Model.Platform;
using System.IO;
using System.Linq;

namespace FSO.SimAntics.Model.TSOPlatform
{
    public class VMTSOObjectState : VMTSOEntityState, VMIObjectState
    {
        public uint OwnerID;

        //repair state
        public ushort Wear { get; set; } = 20 * 4; //times 4. increases by 1 per qtr day.
        public byte QtrDaysSinceLastRepair = 0; //when > 7*4, object can break again.
        public bool Broken
        {
            get
            {
                return QtrDaysSinceLastRepair == 255;
            }
        }

        public VMTSOObjectFlags ObjectFlags;
        public byte UpgradeLevel;

        public VMTSOObjectState() { }

        public VMTSOObjectState(int version) : base(version) { }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            OwnerID = reader.ReadUInt32();

            if (Version > 19)
            {
                Wear = reader.ReadUInt16();
                QtrDaysSinceLastRepair = reader.ReadByte();
            }

            if (Version > 30)
            {
                ObjectFlags = (VMTSOObjectFlags)reader.ReadByte();
            }

            if (Version > 33)
            {
                UpgradeLevel = reader.ReadByte();
            }
        }

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(OwnerID);

            writer.Write(Wear);
            writer.Write(QtrDaysSinceLastRepair);

            writer.Write((byte)ObjectFlags);
            writer.Write(UpgradeLevel);
        }

        public override void Tick(VM vm, object owner)
        {
            base.Tick(vm, owner);
        }

        public void ProcessQTRDay(VM vm, VMEntity owner) {
            if (((VMGameObject)owner).Disabled > 0) return;
            if (ObjectFlags.HasFlag(VMTSOObjectFlags.FSODonated))
            {
                Wear = 0;
                QtrDaysSinceLastRepair = 0;
                return;
            }
            Wear += 1;
            if (Wear > 90 * 4) Wear = 90 * 4;

            if (QtrDaysSinceLastRepair <= 7 * 4)
            {
                QtrDaysSinceLastRepair++;
            }
            
            //can break if the object has a repair interaction.
            if (QtrDaysSinceLastRepair > 7*4 && Wear > 50*4 && owner.TreeTable?.Interactions?.Any(x => (x.Flags & TTABFlags.TSOIsRepair) > 0) == true)
            {
                //object can break. calculate probability
                var rand = (int)vm.Context.NextRandom(10000);
                //lerp
                //1% at 50%, 4% at 90%
                var prob = 100 + ((Wear - (50 * 4)) * 75) / 40;
                if (rand < prob && owner.MultitileGroup.BaseObject == owner)
                {
                    Break(owner);
                }
            }
        }

        public void Break(VMEntity owner)
        {
            //break the object
            QtrDaysSinceLastRepair = 255;
            //apply the broken object particle to all parts
            foreach (var item in owner.MultitileGroup.Objects)
            {
                ((VMGameObject)item).EnableParticle(256);
            }
        }

        public void Donate(VM vm, VMEntity owner)
        {
            //remove all sellback value and set it as donated.
            owner.MultitileGroup.InitialPrice = 0;
            foreach (var obj in owner.MultitileGroup.Objects)
            {
                (obj.TSOState as VMTSOObjectState).ObjectFlags |= VMTSOObjectFlags.FSODonated;
            }
            VMBuildableAreaInfo.UpdateOverbudgetObjects(vm);
        }
    }

    public enum VMTSOObjectFlags : byte
    {
        FSODonated = 1
    }
}
