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
using System.Net;
using Mina.Core.Buffer;
using Mina.Filter.Logging;
using Mina.Transport.Socket;

namespace Mina.Example.Udp
{
    /// <summary>
    /// The class that will accept and process clients in order to properly
    /// track the memory usage.
    /// </summary>
    class MemoryMonitor
    {
        public const int port = 18567;

        static void Main(string[] args)
        {
            AsyncDatagramAcceptor acceptor = new AsyncDatagramAcceptor();

            acceptor.FilterChain.AddLast("logger", new LoggingFilter());
            acceptor.SessionConfig.ReuseAddress = true;

            acceptor.ExceptionCaught += (s, e) =>
            {
                Console.WriteLine(e.Exception);
                e.Session.Close(true);
            };
            acceptor.MessageReceived += (s, e) =>
            {
                IoBuffer buf = e.Message as IoBuffer;
                if (buf != null)
                {
                    Console.WriteLine("New value for {0}: {1}", e.Session.RemoteEndPoint, buf.GetInt64());
                }
            };
            acceptor.SessionCreated += (s, e) =>
            {
                Console.WriteLine("Session created...");
            };
            acceptor.SessionOpened += (s, e) =>
            {
                Console.WriteLine("Session opened...");
            };
            acceptor.SessionClosed += (s, e) =>
            {
                Console.WriteLine("Session closed...");
            };
            acceptor.SessionIdle += (s, e) =>
            {
                Console.WriteLine("Session idle...");
            };

            acceptor.Bind(new IPEndPoint(IPAddress.Any, port));
            Console.WriteLine("UDPServer listening on port " + port);
            Console.ReadLine();
        }
    }
}
