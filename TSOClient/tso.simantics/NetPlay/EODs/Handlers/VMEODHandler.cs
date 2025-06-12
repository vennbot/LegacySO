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

namespace FSO.SimAntics.NetPlay.EODs.Handlers
{
    public abstract class VMEODHandler
    {
        public Dictionary<string, EODPlaintextEventHandler> PlaintextHandlers;
        public Dictionary<string, EODBinaryEventHandler> BinaryHandlers;
        public Dictionary<short, EODSimanticsEventHandler> SimanticsHandlers;

        public VMEODServer Server;

        public VMEODHandler(VMEODServer server)
        {
            PlaintextHandlers = new Dictionary<string, EODPlaintextEventHandler>();
            BinaryHandlers = new Dictionary<string, EODBinaryEventHandler>();
            SimanticsHandlers = new Dictionary<short, EODSimanticsEventHandler>();
            Server = server;
        }

        public virtual void OnConnection(VMEODClient client)
        {

        }

        public virtual void OnDisconnection(VMEODClient client)
        {

        }

        public virtual void SelfResync()
        {
            //in some cases, the server might want to save and instantly reload its lot state to fix
            //lingering issues with "unsavable" state causing desyncs.

            //in this case, we need to make sure all eodclients are pointing to the new object.
            //when this is called, they should be pointing to the old objects we already overwrote
            //but their object ids should be the same in the new state, so we should just be able
            //to get them again from the VM.

            if (Server.Object != null) Server.Object = Server.vm.GetObjectById(Server.Object.ObjectID);
            foreach (var client in Server.Clients)
            {
                if (client.Invoker != null) client.Invoker = Server.vm.GetObjectById(client.Invoker.ObjectID);
                if (client.Avatar != null) client.Avatar = (VMAvatar)Server.vm.GetObjectById(client.Avatar.ObjectID);
            }
        }

        public virtual void Tick()
        {

        }
    }


    public delegate void EODPlaintextEventHandler(string evt, string body, VMEODClient client);
    public delegate void EODBinaryEventHandler(string evt, byte[] body, VMEODClient client);
    public delegate void EODSimanticsEventHandler(short evt, VMEODClient client);

    public delegate void EODDirectPlaintextEventHandler(string evt, string body);
    public delegate void EODDirectBinaryEventHandler(string evt, byte[] body);
}
