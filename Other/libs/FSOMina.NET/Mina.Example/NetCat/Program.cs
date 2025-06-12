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
using Mina.Core.Future;
using Mina.Core.Session;
using Mina.Transport.Socket;

namespace Mina.Example.NetCat
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine(typeof(Program).FullName + " <hostname> <port>");
                return;
            }

            // Create TCP/IP connector.
            AsyncSocketConnector connector = new AsyncSocketConnector();

            // Set connect timeout.
            connector.ConnectTimeoutInMillis = 30 * 1000L;

            // Set reader idle time to 10 seconds.
            // sessionIdle(...) method will be invoked when no data is read
            // for 10 seconds.
            connector.SessionOpened += (s, e) => e.Session.Config.SetIdleTime(IdleStatus.ReaderIdle, 10);

            // Print out total number of bytes read from the remote peer.
            connector.SessionClosed += (s, e) => Console.WriteLine("Total " + e.Session.ReadBytes + " byte(s)");

            connector.SessionIdle += (s, e) => 
            {
                if (e.IdleStatus == IdleStatus.ReaderIdle)
                    e.Session.Close(true);
            };

            connector.MessageReceived += (s, e) =>
            {
                IoBuffer buf = (IoBuffer)e.Message;
                while (buf.HasRemaining)
                {
                    Console.Write((Char)buf.Get());
                }
            };

            // Start communication.
            IConnectFuture cf = connector.Connect(new IPEndPoint(Dns.GetHostEntry(args[0]).AddressList[3], Int32.Parse(args[1])));

            // Wait for the connection attempt to be finished.
            cf.Await();
            cf.Session.CloseFuture.Await();

            connector.Dispose();
        }
    }
}
