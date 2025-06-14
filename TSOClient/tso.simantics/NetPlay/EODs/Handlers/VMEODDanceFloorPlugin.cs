
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
using FSO.SimAntics.NetPlay.EODs.Model;

namespace FSO.SimAntics.NetPlay.EODs.Handlers
{
    public class VMEODDanceFloorPlugin : VMEODHandler
    {
        public VMEODClient ControllerClient;
        public VMEODDanceFloorPlugin(VMEODServer server) : base(server)
        {
            PlaintextHandlers["close"] = P_Close;
            PlaintextHandlers["press_button"] = P_DanceButton;
        }

        public void P_Close(string evt, string text, VMEODClient client)
        {
            Server.Disconnect(client);
        }

        public void P_DanceButton(string evt, string text, VMEODClient client)
        {
            byte num;
            if (!byte.TryParse(text, out num)) return;
            if (ControllerClient != null) ControllerClient.SendOBJEvent(new VMEODEvent(num, client.Avatar.ObjectID));
        }

        public override void OnConnection(VMEODClient client)
        {
            if (client.Avatar != null)
            {
                client.Send("dance_show", "");
            }
            else
            {
                //we're the dance floor controller!
                ControllerClient = client;
            }
        }
    }
}
