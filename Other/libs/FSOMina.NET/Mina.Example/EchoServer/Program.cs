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
using System.Threading;
using Mina.Core.Buffer;
using Mina.Core.Service;
using Mina.Core.Session;
using Mina.Filter.Logging;
using Mina.Filter.Ssl;
using Mina.Transport.Socket;

namespace Mina.Example.EchoServer
{
    class Program
    {
        private static readonly int port = 8080;
        private static readonly Boolean ssl = false;

        static void Main(string[] args)
        {
            IoAcceptor acceptor = new AsyncSocketAcceptor();

            if (ssl)
                acceptor.FilterChain.AddLast("ssl", new SslFilter(AppDomain.CurrentDomain.BaseDirectory + "\\TempCert.cer"));

            acceptor.FilterChain.AddLast("logger", new LoggingFilter());

            acceptor.Activated += (s, e) => Console.WriteLine("ACTIVATED");
            acceptor.Deactivated += (s, e) => Console.WriteLine("DEACTIVATED");
            acceptor.SessionCreated += (s, e) => e.Session.Config.SetIdleTime(IdleStatus.BothIdle, 10);
            acceptor.SessionOpened += (s, e) => Console.WriteLine("OPENED");
            acceptor.SessionClosed += (s, e) => Console.WriteLine("CLOSED");
            acceptor.SessionIdle += (s, e) => Console.WriteLine("*** IDLE #" + e.Session.GetIdleCount(IdleStatus.BothIdle) + " ***");
            acceptor.ExceptionCaught += (s, e) => e.Session.Close(true);
            acceptor.MessageReceived += (s, e) =>
            {
                Console.WriteLine("Received : " + e.Message);
                IoBuffer income = (IoBuffer)e.Message;
                IoBuffer outcome = IoBuffer.Allocate(income.Remaining);
                outcome.Put(income);
                outcome.Flip();
                e.Session.Write(outcome);
            };

            acceptor.Bind(new IPEndPoint(IPAddress.Any, port));

            Console.WriteLine("Listening on " + acceptor.LocalEndPoint);

            while (true)
            {
                Console.WriteLine("R: " + acceptor.Statistics.ReadBytesThroughput +
                    ", W: " + acceptor.Statistics.WrittenBytesThroughput);
                Thread.Sleep(3000);
            }
        }
    }
}
