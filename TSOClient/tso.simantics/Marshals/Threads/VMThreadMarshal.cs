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
using FSO.SimAntics.Engine;
using FSO.SimAntics.NetPlay.Model;
using System;
using System.IO;
using FSO.SimAntics.Model.TSOPlatform;
using FSO.SimAntics.NetPlay.EODs.Model;

namespace FSO.SimAntics.Marshals.Threads
{
    public class VMThreadMarshal : VMSerializable
    {
        public VMStackFrameMarshal[] Stack;
        public VMQueuedActionMarshal[] Queue;
        public sbyte ActiveQueueBlock;
        public short[] TempRegisters = new short[20];
        public int[] TempXL = new int[2];
        public VMPrimitiveExitCode LastStackExitCode = VMPrimitiveExitCode.GOTO_FALSE;

        public VMAsyncState BlockingState; //NULLable
        public VMEODPluginThreadState EODConnection; //NULLable
        public bool Interrupt;

        public ushort ActionUID;
        public int DialogCooldown;

        public uint ScheduleIdleStart;

        public int Version;

        public VMThreadMarshal() { }
        public VMThreadMarshal(int version) { Version = version; }

        public void SerializeInto(BinaryWriter writer)
        {
            writer.Write(Stack.Length);
            foreach (var item in Stack)
            {
                byte frameType = 0;

                if (item is VMRoutingFrameMarshal)
                {
                    frameType = 1;
                }
                else if (item is VMDirectControlFrameMarshal)
                {
                    frameType = 2;
                }

                writer.Write(frameType);
                item.SerializeInto(writer);
            }

            writer.Write(Queue.Length);
            foreach (var item in Queue) item.SerializeInto(writer);
            writer.Write(ActiveQueueBlock);

            writer.Write(VMSerializableUtils.ToByteArray(TempRegisters));
            foreach (var item in TempXL) writer.Write(item);
            writer.Write((byte)LastStackExitCode);

            writer.Write(BlockingState != null);
            if (BlockingState != null) VMAsyncState.SerializeGeneric(writer, BlockingState);
            writer.Write(EODConnection != null);
            if (EODConnection != null) EODConnection.SerializeInto(writer);
            writer.Write(Interrupt);

            writer.Write(ActionUID);
            writer.Write(DialogCooldown);

            writer.Write(ScheduleIdleStart);
        }

        public void Deserialize(BinaryReader reader)
        {
            var stackN = reader.ReadInt32();
            Stack = new VMStackFrameMarshal[stackN];
            for (int i = 0; i < stackN; i++)
            {
                var type = reader.ReadByte();

                VMStackFrameMarshal frame;
                switch (type)
                {
                    case 0: frame = new VMStackFrameMarshal(Version); break;
                    case 1: frame = new VMRoutingFrameMarshal(Version); break;
                    case 2: frame = new VMDirectControlFrameMarshal(Version); break;
                    default: throw new Exception($"Unsupported stack frame type {i}.");
                }

                Stack[i] = frame;
                Stack[i].Deserialize(reader);
            }

            var queueN = reader.ReadInt32();
            Queue = new VMQueuedActionMarshal[queueN];
            for (int i = 0; i < queueN; i++)
            {
                Queue[i] = new VMQueuedActionMarshal(Version);
                Queue[i].Deserialize(reader);
            }
            if (Version > 4) ActiveQueueBlock = reader.ReadSByte();

            TempRegisters = new short[20];
            for (int i = 0; i < 20; i++) TempRegisters[i] = reader.ReadInt16();
            TempXL = new int[2];
            for (int i = 0; i < 2; i++) TempXL[i] = reader.ReadInt32();
            LastStackExitCode = (VMPrimitiveExitCode)reader.ReadByte();

            if (reader.ReadBoolean()) BlockingState = VMAsyncState.DeserializeGeneric(reader, Version);
            else BlockingState = null;
            if (Version > 2 && reader.ReadBoolean())
            {
                EODConnection = new VMEODPluginThreadState();
                EODConnection.Version = Version;
                EODConnection.Deserialize(reader);
            }
            
            Interrupt = reader.ReadBoolean();

            ActionUID = reader.ReadUInt16();
            DialogCooldown = reader.ReadInt32();
            if (Version > 15) ScheduleIdleStart = reader.ReadUInt32();
        }
    }
}
