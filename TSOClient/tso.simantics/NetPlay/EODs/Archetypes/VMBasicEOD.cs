
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
using FSO.SimAntics.NetPlay.EODs.Handlers;
using FSO.SimAntics.NetPlay.EODs.Utils;

namespace FSO.SimAntics.NetPlay.EODs.Archetypes
{
    public class VMBasicEOD <T> : VMEODHandler
    {
        protected EODLobby<T> Lobby;
        protected string EODName;

        public VMBasicEOD(VMEODServer server, string name) : base(server)
        {
            EODName = name;

            Lobby = new EODLobby<T>(server, 1)
                    .OnJoinSend(EODName + "_show")
                    .OnFailedToJoinDisconnect();

            PlaintextHandlers["close"] = Lobby.Close;
        }

        protected virtual void OnConnected(VMEODClient client)
        {
        }

        public override void OnConnection(VMEODClient client)
        {
            if (client.Avatar != null)
            {
                if (Lobby.Join(client, 0))
                {
                    OnConnected(client);
                }
            }
        }

        public override void OnDisconnection(VMEODClient client)
        {
            Lobby.Leave(client);
        }
    }
}
