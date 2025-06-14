
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
using FSO.SimAntics.Model;
using System;
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetSkillLockCmd : VMNetCommandBodyAbstract
    {
        public byte SkillID;
        public short LockLevel;

        private static VMPersonDataVariable[] LockToSkill =
        {
            VMPersonDataVariable.BodySkill,
            VMPersonDataVariable.CharismaSkill,
            VMPersonDataVariable.CookingSkill,
            VMPersonDataVariable.CreativitySkill,
            VMPersonDataVariable.LogicSkill,
            VMPersonDataVariable.MechanicalSkill
        };

        public override bool Execute(VM vm, VMAvatar caller)
        {
            //need caller to be present
            if (caller == null) return false;
            var limit = caller.SkillLocks;
            SkillID = Math.Min(SkillID, (byte)5); //must be 0-5
            int otherLocked = 0;
            for (int i=0; i<6; i++) //sum other skill locks to see what we can feasibly put in this skill
            {
                if (i == SkillID) continue;
                otherLocked += caller.GetPersonData((VMPersonDataVariable)((int)VMPersonDataVariable.SkillLockBase + i))/100;
            }
            if (otherLocked >= limit) return false; //cannot lock this skill at all
            LockLevel = (short)Math.Min(caller.GetPersonData(LockToSkill[SkillID])/100, Math.Min(LockLevel, (short)(limit - otherLocked))); //can only lock up to the limit

            caller.SetPersonData((VMPersonDataVariable)((int)VMPersonDataVariable.SkillLockBase + SkillID), (short)(LockLevel*100));
            return true;
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            SkillID = reader.ReadByte();
            LockLevel = reader.ReadInt16();
        }

        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            writer.Write(SkillID);
            writer.Write(LockLevel);
        }
    }
}
