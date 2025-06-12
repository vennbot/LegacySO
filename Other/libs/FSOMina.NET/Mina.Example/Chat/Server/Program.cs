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
using Mina.Core.Service;
using Mina.Filter.Codec;
using Mina.Filter.Codec.TextLine;
using Mina.Filter.Logging;
using Mina.Filter.Ssl;
using Mina.Transport.Socket;

namespace Mina.Example.Chat.Server
{
    class Program
    {
        private static readonly int port = 1234;
        private static readonly Boolean ssl = true;

        static void Main(string[] args)
        {
            IoAcceptor acceptor = new AsyncSocketAcceptor();

            if (ssl)
            {
                acceptor.FilterChain.AddLast("ssl", new SslFilter(AppDomain.CurrentDomain.BaseDirectory + "\\TempCert.cer"));
                Console.WriteLine("SSL ON");
            }

            acceptor.FilterChain.AddLast("logger", new LoggingFilter());
            acceptor.FilterChain.AddLast("codec", new ProtocolCodecFilter(new TextLineCodecFactory()));

            acceptor.Handler = new ChatProtocolHandler();

            acceptor.Bind(new IPEndPoint(IPAddress.Any, port));

            Console.WriteLine("Listening on " + acceptor.LocalEndPoint);

            Console.ReadLine();
        }
    }
}
