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
using Mina.Core.Service;
using Mina.Core.Session;

namespace Mina.Example.GettingStarted.TimeServer
{
    /// <summary>
    /// The Time Server handler : it return the current date when a message is received,
    /// or close the session if the "quit" message is received.
    /// </summary>
    class TimeServerHandler : IoHandlerAdapter
    {
        /// <summary>
        /// Trap exceptions.
        /// </summary>
        public override void ExceptionCaught(IoSession session, Exception cause)
        {
            Console.WriteLine(cause);
        }

        /// <summary>
        /// If the message is 'quit', we exit by closing the session. Otherwise,
        /// we return the current date.
        /// </summary>
        public override void MessageReceived(IoSession session, Object message)
        {
            String str = message.ToString();

            // "Quit" ? let's get out ...
            if (str.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                session.Close(true);
                return;
            }

            // Send the current date back to the client
            session.Write(DateTime.Now.ToString());
            Console.WriteLine("Message written...");
        }

        /// <summary>
        /// On idle, we just write a message on the console
        /// </summary>
        public override void SessionIdle(IoSession session, IdleStatus status)
        {
            Console.WriteLine("IDLE " + session.GetIdleCount(status));
        }
    }
}
