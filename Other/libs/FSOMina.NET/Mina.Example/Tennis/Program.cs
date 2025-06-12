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
using Mina.Core.Future;
using Mina.Core.Service;
using Mina.Core.Session;
using Mina.Transport.Loopback;

namespace Mina.Example.Tennis
{
    class Program
    {
        static void Main(string[] args)
        {
            IoAcceptor acceptor = new LoopbackAcceptor();
            LoopbackEndPoint lep = new LoopbackEndPoint(8080);

            // Set up server
            acceptor.Handler = new TennisPlayer();
            acceptor.Bind(lep);

            // Connect to the server.
            LoopbackConnector connector = new LoopbackConnector();
            connector.Handler = new TennisPlayer();
            IConnectFuture future = connector.Connect(lep);
            future.Await();
            IoSession session = future.Session;

            // Send the first ping message
            session.Write(new TennisBall(10));

            // Wait until the match ends.
            session.CloseFuture.Await();

            acceptor.Unbind();
        }
    }
}
