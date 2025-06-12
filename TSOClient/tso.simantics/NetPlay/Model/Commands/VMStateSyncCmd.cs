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
using FSO.Common.Utils;
using FSO.SimAntics.Engine.Debug;
using FSO.SimAntics.Marshals;
using FSO.SimAntics.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FSO.SimAntics.NetPlay.Model.Commands
{
    public class VMStateSyncCmd : VMNetCommandBodyAbstract
    {
        public VMMarshal State;
        public bool Run = true;
        public List<VMSyncTraceTick> Traces;

        //very important: we can't deserialize state information from the client. They might try to exhaust our memory, take a huge amount of our time or do bad things!
        public override bool AcceptFromClient { get { return false; } }

        public override bool Execute(VM vm)
        {
            if (Traces != null && vm.Driver.DesyncTick != 0) vm.Trace.CompareFirstError(Traces.FirstOrDefault(x => x.TickID == vm.Driver.DesyncTick));

            vm.Driver.DesyncTick = 0;
            if (!Run) return true;

            if (vm.FSOVDoAsyncLoad)
            {
                vm.FSOVDoAsyncLoad = false;
                vm.FSOVAsyncLoading = true;
                Task.Run(() =>
                {
                    vm.FSOVClientJoin = (vm.Context.Architecture == null);
                    vm.LoadAsync(State);
                    if (VM.UseWorld && vm.Context.Blueprint.SubWorlds.Count == 0) VMLotTerrainRestoreTools.RestoreSurroundings(vm, vm.HollowAdj);
                    GameThread.InUpdate(() =>
                    {
                        vm.LoadComplete();
                    });
                });
            }
            else
            {
                vm.Load(State);
                if (VM.UseWorld && vm.Context.Blueprint.SubWorlds.Count == 0) VMLotTerrainRestoreTools.RestoreSurroundings(vm, vm.HollowAdj);
            }
            return true;
        }

        public override bool Verify(VM vm, VMAvatar caller)
        {
            return !FromNet;
        }

        #region VMSerializable Members
        public override void Deserialize(BinaryReader reader)
        {
            State = new VMMarshal();
            State.Deserialize(reader);
            if (reader.ReadBoolean())
            {
                Traces = new List<VMSyncTraceTick>();
                var total = reader.ReadInt32();
                for (int i = 0; i < total; i++)
                {
                    var trace = new VMSyncTraceTick();
                    trace.Deserialize(reader);
                    Traces.Add(trace);
                }
            }
        }

        public override void SerializeInto(BinaryWriter writer)
        {
            State.SerializeInto(writer);
            writer.Write(Traces != null);
            if (Traces != null)
            {
                writer.Write(Traces.Count);
                foreach (var trace in Traces)
                {
                    trace.SerializeInto(writer);
                }
            }
        }
        #endregion
    }
}
