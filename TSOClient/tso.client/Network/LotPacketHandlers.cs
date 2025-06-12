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
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using TSOClient.VM;
using TSOClient.Lot;

namespace TSOClient.Network
{
    class LotPacketHandlers
    {
        public static void OnSimulationState(NetworkClient Client, PacketStream Packet, LotScreen Lot)
        {
            List<SimulationObject> SimObjects = new List<SimulationObject>();

            byte Opcode = (byte)Packet.ReadByte();

            byte NumTicks = (byte)Packet.ReadByte();
            int NumObjects = Packet.ReadInt32();
            BinaryFormatter BinFormatter = new BinaryFormatter();

            for (int i = 0; i < NumObjects; i++)
            {
                SimulationObject SimObject = (SimulationObject)BinFormatter.Deserialize(Packet);
                SimObjects.Add(SimObject);
            }

            Lot.UpdateSimulationState(NumTicks, SimObjects);
        }
    }
}
