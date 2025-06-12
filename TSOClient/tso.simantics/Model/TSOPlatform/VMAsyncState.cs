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
using FSO.SimAntics.Primitives;
using FSO.SimAntics.NetPlay.EODs.Model;

namespace FSO.SimAntics.Model.TSOPlatform
{
    public class VMAsyncState : VMSerializable
    {
        public static Dictionary<VMAsyncStateType, Type> TypeResolve = new Dictionary<VMAsyncStateType, System.Type>()
        {
            { VMAsyncStateType.TransferFunds, typeof(VMTransferFundsState) },
            { VMAsyncStateType.DialogResult, typeof(VMDialogResult) },
            { VMAsyncStateType.PluginState, typeof(VMEODPluginThreadState) },
            { VMAsyncStateType.InventoryOp, typeof(VMInventoryOpState) }

        };
        public static Dictionary<Type, VMAsyncStateType> TypeMarshal = TypeResolve.ToDictionary(x => x.Value, x => x.Key);

        public static VMAsyncState DeserializeGeneric(BinaryReader reader, int version)
        {
            var type = (VMAsyncStateType)reader.ReadByte();
            Type cmdType = TypeResolve[type];
            var state = (VMAsyncState)Activator.CreateInstance(cmdType);
            state.Version = version;
            state.Deserialize(reader);
            return state;
        }

        public static void SerializeGeneric(BinaryWriter writer, VMAsyncState state)
        {
            writer.Write((byte)TypeMarshal[state.GetType()]);
            state.SerializeInto(writer);
        }

        public bool Responded;
        public int WaitTime;
        public int Version;

        public virtual void Deserialize(BinaryReader reader)
        {
            Responded = reader.ReadBoolean();
            if (Version > 1) WaitTime = reader.ReadInt32();
        }

        public virtual void SerializeInto(BinaryWriter writer)
        {
            writer.Write(Responded);
            writer.Write(WaitTime);
        }
    }

    public enum VMAsyncStateType : byte
    {
        TransferFunds = 0,
        DialogResult = 1,
        PluginState = 2,
        InventoryOp = 3
    }
}
