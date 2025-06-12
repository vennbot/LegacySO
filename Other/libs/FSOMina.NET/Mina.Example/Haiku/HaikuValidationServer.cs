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
using System.Text;
using Mina.Core.Service;
using Mina.Filter.Codec;
using Mina.Filter.Codec.TextLine;
using Mina.Filter.Logging;
using Mina.Transport.Socket;
using Mina.Filter.Executor;

namespace Mina.Example.Haiku
{
    class HaikuValidationServer
    {
        private const int port = 42458;

        static void Main(string[] args)
        {
            /*
             * ReuseBuffer needs to be false since we have a ExecutorFilter before
             * ProtocolCodecFilter which processes incoming IoBuffer.
             */
            IoAcceptor acceptor = new AsyncSocketAcceptor() { ReuseBuffer = false };

            acceptor.FilterChain.AddLast("logger", new LoggingFilter());
            acceptor.FilterChain.AddLast("executor", new ExecutorFilter());
            acceptor.FilterChain.AddLast("to-string", new ProtocolCodecFilter(
                new TextLineCodecFactory(Encoding.UTF8)));
            acceptor.FilterChain.AddLast("to-haiki", new ToHaikuIoFilter());

            acceptor.ExceptionCaught += (s, e) => e.Session.Close(true);

            HaikuValidator validator = new HaikuValidator();
            acceptor.MessageReceived += (s, e) =>
            {
                Haiku haiku = (Haiku)e.Message;

                try
                {
                    validator.Validate(haiku);
                    e.Session.Write("HAIKU!");
                }
                catch (InvalidHaikuException ex)
                {
                    e.Session.Write("NOT A HAIKU: " + ex.Message);
                }
            };

            acceptor.Bind(new IPEndPoint(IPAddress.Any, port));

            Console.WriteLine("Listening on " + acceptor.LocalEndPoint);
            Console.ReadLine();
        }
    }
}
