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
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mina.Core.Service;
using Mina.Core.Session;

namespace Mina.Example.Chat.Server
{
    class ChatProtocolHandler : IoHandlerAdapter
    {
#if NET20
        IDictionary<IoSession, Boolean> sessions = new Dictionary<IoSession, Boolean>();
        IDictionary<String, Boolean> users = new Dictionary<String, Boolean>();
#else
        IDictionary<IoSession, Boolean> sessions = new ConcurrentDictionary<IoSession, Boolean>();
        IDictionary<String, Boolean> users = new ConcurrentDictionary<String, Boolean>();
#endif

        public void Broadcast(String message)
        {
            foreach (IoSession session in sessions.Keys)
            {
                if (session.Connected)
                    session.Write("BROADCAST OK " + message);
            }
        }

        public override void ExceptionCaught(IoSession session, Exception cause)
        {
            Console.WriteLine("Unexpected exception." + cause);
            session.Close(true);
        }

        public override void SessionClosed(IoSession session)
        {
            String user = session.GetAttribute<String>("user");
            sessions.Remove(session);
            if (user != null)
            {
                users.Remove(user);
                Broadcast("The user " + user + " has left the chat session.");
            }
        }

        public override void MessageReceived(IoSession session, Object message)
        {
            String theMessage = (String)message;
            String[] result = theMessage.Split(new Char[] { ' ' }, 2);
            String theCommand = result[0];

            String user = session.GetAttribute<String>("user");

            if (String.Equals("QUIT", theCommand, StringComparison.OrdinalIgnoreCase))
            {
                session.Write("QUIT OK");
                session.Close(true);
            }
            else if (String.Equals("LOGIN", theCommand, StringComparison.OrdinalIgnoreCase))
            {
                if (user != null)
                {
                    session.Write("LOGIN ERROR user " + user + " already logged in.");
                    return;
                }

                if (result.Length == 2)
                {
                    user = result[1];
                }
                else
                {
                    session.Write("LOGIN ERROR invalid login command.");
                    return;
                }

                // check if the username is already used
                if (users.ContainsKey(user))
                {
                    session.Write("LOGIN ERROR the name " + user + " is already used.");
                    return;
                }

                sessions[session] = true;
                session.SetAttribute("user", user);

                // Allow all users
                users[user] = true;
                session.Write("LOGIN OK");
                Broadcast("The user " + user + " has joined the chat session.");
            }
            else if (String.Equals("BROADCAST", theCommand, StringComparison.OrdinalIgnoreCase))
            {
                if (result.Length == 2)
                {
                    Broadcast(user + ": " + result[1]);
                }
            }
            else
            {
                Console.WriteLine("Unhandled command: " + theCommand);
            }
        }
    }
}
