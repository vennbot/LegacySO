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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityDataModel
{
    public delegate void MessageLoggedDelegate(LogMessage Msg);

    public enum LogLevel
	{
		error=1
		,warn
		,info
		,verbose
	}

    public class LogMessage
    {
        public LogMessage(string Msg, LogLevel Lvl)
        {
            Message = Msg;
            Level = Lvl;
        }

        public string Message;
        public LogLevel Level;
    }

    /// <summary>
    /// A class for subscribing to messages logged by GonzoNet.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Subscribe to this event to receive messages logged by GonzoNet.
        /// </summary>
        public static event MessageLoggedDelegate OnMessageLogged;

        /// <summary>
        /// Called by classes in GonzoNet to log a message.
        /// </summary>
        /// <param name="Msg"></param>
        public static void Log(string Message, LogLevel Lvl)
        {
            if (GlobalSettings.Default.DEBUG_BUILD)
            {
                LogMessage Msg = new LogMessage(Message, Lvl);
                OnMessageLogged(Msg);
            }
        }
    }
}
