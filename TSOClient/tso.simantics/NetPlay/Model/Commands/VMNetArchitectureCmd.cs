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
using System.Collections.Generic;
using System.IO;
using FSO.SimAntics.Model;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMNetArchitectureCmd : VMNetCommandBodyAbstract
    {
        public List<VMArchitectureCommand> Commands;
        public bool Verified;

        public override bool Execute(VM vm)
        {
            for (int i = 0; i < Commands.Count; i++)
            {
                var cmd = Commands[i];
                cmd.CallerUID = ActorUID;
                Commands[i] = cmd;
            }
            vm.Context.Architecture.RunCommands(Commands, false);
            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            //since architecture commands must be run in order, we need to run all architecture commands synchronously.
            //it must be queued on the global link.

            if (Verified) return true;
            if (!vm.TS1 && (caller == null || //caller must be on lot, have build permissions
                !vm.PlatformState.Validator.CanBuildTool(caller))) return false; 

            for (int i = 0; i < Commands.Count; i++)
            {
                var cmd = Commands[i];
                cmd.CallerUID = ActorUID;
                Commands[i] = cmd;
            }

            vm.GlobalLink.QueueArchitecture(this);

            return false;
        }

        #region VMSerializable Members
        public override void SerializeInto(BinaryWriter writer)
        {
            base.SerializeInto(writer);
            if (Commands == null) writer.Write(0);
            else
            {
                writer.Write(Commands.Count);
                for (int i=0; i<Commands.Count; i++)
                {
                    Commands[i].SerializeInto(writer);
                }
            }
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            Commands = new List<VMArchitectureCommand>();
            int length = reader.ReadInt32();
            for (int i=0; i<length; i++)
            {
                var cmd = new VMArchitectureCommand();
                cmd.Deserialize(reader);
                Commands.Add(cmd);
            }
        }
        #endregion

    }
}
