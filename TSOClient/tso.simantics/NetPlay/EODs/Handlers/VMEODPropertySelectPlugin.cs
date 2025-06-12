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
using System.Linq;

namespace FSO.SimAntics.NetPlay.EODs.Handlers
{
    internal class VMEODPropertySelectPlugin : VMEODHandler
    {
        private static int MaxLotNameSize = 128;

        // Allows the user to select a property via search.
        // temp 0 contains low ID
        // temp 1 contains high ID

        public uint LotID;
        private bool SentMessage = false;

        public VMEODPropertySelectPlugin(VMEODServer server) : base(server)
        {
            BinaryHandlers["property_select"] = B_Select;
            PlaintextHandlers["close"] = P_Close;
        }

        public override void OnConnection(VMEODClient client)
        {
            var param = client.Invoker.Thread.TempRegisters;
            LotID = (uint)((((int)param[1]) << 16) | (int)param[0]);
        }

        public override void Tick()
        {
            lock (this)
            {
                if (!SentMessage)
                {
                    var client = Server.Clients.FirstOrDefault();
                    if (client == null) return; //uh, what?

                    var data = BitConverter.GetBytes(LotID);

                    client.Send("property_show", data);
                    SentMessage = true;
                }
            }
        }

        public void B_Select(string evt, byte[] data, VMEODClient client)
        {
            if (data.Length < 4 || data.Length > MaxLotNameSize + 4)
            {
                // Not a uint.
                return;
            }

            LotID = BitConverter.ToUInt32(data, 0);

            client.SendOBJEvent(new Model.VMEODEvent((short)VMEODPropertySelectEvents.UpdateLotId, (short)LotID, (short)(LotID >> 16)));

            for (int i = 4; i < data.Length; i++)
            {
                client.SendOBJEvent(new Model.VMEODEvent((short)VMEODPropertySelectEvents.PushStringByte, data[i]));
            }
        }

        public void P_Close(string evt, string text, VMEODClient client)
        {
            Server.Shutdown();
        }
    }

    public enum VMEODPropertySelectEvents : short
    {
        UpdateLotId = 1,
        PushStringByte = 2
    }
}
