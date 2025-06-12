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
using FSO.SimAntics.NetPlay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FSO.SimAntics.Engine.Debug
{
    public class VMSyncTrace
    {
        public List<VMSyncTraceTick> History = new List<VMSyncTraceTick>();
        private VMSyncTraceTick Current = null;
        private static readonly int MAX_HISTORY = 30 * 60;
        
        public VMSyncTraceTick GetTick(uint tickID)
        {
            return History.FirstOrDefault(x => x.TickID == tickID);
        }

        public void NewTick(uint id)
        {
            Current = new VMSyncTraceTick() { TickID = id };
            History.Add(Current);
            if (History.Count > MAX_HISTORY) History.RemoveAt(0);
        }

        public void Trace(string str)
        {
            Current?.Trace.Add(str);
        }

        public void CompareFirstError(VMSyncTraceTick compare)
        {
            var me = GetTick(compare.TickID);
            if (me == null) return;
            var last = "<start tick>";
            for (int i = 0; i < compare.Trace.Count && i < me.Trace.Count; i++)
            {
                if (compare.Trace[i] != me.Trace[i])
                {
                    Console.WriteLine("!!! DESYNC DETECTED !!!");
                    Console.WriteLine("Last:");
                    Console.WriteLine(last);
                    Console.WriteLine("Our trace:");
                    Console.WriteLine(me.Trace[i]);
                    Console.WriteLine("Server trace:");
                    Console.WriteLine(compare.Trace[i]);
                    return;
                }
                last = me.Trace[i];
            }
        }
    }

    public class VMSyncTraceTick : VMSerializable
    {
        public uint TickID = 0;
        public List<string> Trace = new List<string>();
        public void Deserialize(BinaryReader reader)
        {
            Trace.Clear();
            TickID = reader.ReadUInt32();
            var count = reader.ReadInt32();
            for (int i=0; i<count; i++)
            {
                Trace.Add(reader.ReadString());
            }
        }

        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(TickID);
            writer.Write(Trace.Count);
            foreach (var item in Trace) writer.Write(item);
        }
    }
}
