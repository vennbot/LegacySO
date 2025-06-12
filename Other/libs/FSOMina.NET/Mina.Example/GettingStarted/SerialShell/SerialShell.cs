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
using System.Text;
using Mina.Core.Future;
using Mina.Core.Service;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.TextLine;
using Mina.Filter.Logging;
using Mina.Transport.Serial;

namespace Mina.Example.GettingStarted.SerialShell
{
    class SerialShell
    {
        static void Main(string[] args)
        {
            IoConnector serial = new SerialConnector();

            // Add two filters : a logger and a codec
            serial.FilterChain.AddLast("logger", new LoggingFilter());
            serial.FilterChain.AddLast("codec", new ProtocolCodecFilter(new TextLineCodecFactory(Encoding.UTF8)));

            serial.ExceptionCaught += (s, e) => e.Session.Close(true);
            serial.MessageReceived += (s, e) =>
            {
                Console.WriteLine(e.Message);
            };

            SerialEndPoint serialEP = new SerialEndPoint("COM3", 38400);
            IConnectFuture future = serial.Connect(serialEP);
            future.Await();

            if (future.Connected)
            {
                while (true)
                {
                    String line = Console.ReadLine();
                    if (line.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
                    {
                        future.Session.Close(true);
                        break;
                    }
                    future.Session.Write(line);
                }
            }
            else if (future.Exception != null)
            {
                Console.WriteLine(future.Exception);
            }

            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }
    }
}
