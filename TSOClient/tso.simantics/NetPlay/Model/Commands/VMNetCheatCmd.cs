
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
using FSO.SimAntics.Utils;
using System.IO;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    /// <summary>
    /// A command submitting a cheat with a typed parameter T
    /// </summary>
    public class VMNetCheatCmd : VMNetCommandBodyAbstract
    {                
        public override bool AcceptFromClient => false; //if a client wants to cheat that should be ignored.

        public VMCheatContext Context;

        public override bool Execute(VM vm, VMAvatar caller)
        {
            if (!vm.TS1 || caller == null || Context == null) return false; //if we aren't in TS1 that would be bad -- invalid if we have no Context
            if (Context.Executed) // the cheat has already been run
                return false;
            return Context.Execute(vm, caller);            
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            if (FromNet) return false; //disable for network play
            //TODO: add Verify() to cheat context to allow cheats to verify themself based off their intended behavior
            return true;
        }

        #region VMSerializeable Members
        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);            
            Context.SerializeInto(writer);
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            Context = new VMCheatContext();
            Context.Deserialize(reader);
        }
        #endregion
    }
}
