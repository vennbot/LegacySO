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
namespace FSO.Server.Common
{
    public class ApiAbstract
    {
        public event APIRequestShutdownDelegate OnRequestShutdown;
        public event APIBroadcastMessageDelegate OnBroadcastMessage;
        public event APIRequestUserDisconnectDelegate OnRequestUserDisconnect;
        public event APIRequestMailNotifyDelegate OnRequestMailNotify;

        public delegate void APIRequestShutdownDelegate(uint time, ShutdownType type);
        public delegate void APIBroadcastMessageDelegate(string sender, string title, string message);
        public delegate void APIRequestUserDisconnectDelegate(uint user_id);
        public delegate void APIRequestMailNotifyDelegate(int message_id, string subject, string body, uint target_id);

        public void RequestShutdown(uint time, ShutdownType type)
        {
            OnRequestShutdown?.Invoke(time, type);
        }

        /// <summary>
        /// Asks the server to disconnect a user.
        /// </summary>
        /// <param name="user_id"></param>
        public void RequestUserDisconnect(uint user_id)
        {
            OnRequestUserDisconnect?.Invoke(user_id);
        }

        /// <summary>
        /// Asks the server to notify the client about the new message.
        /// </summary>
        /// <param name="message_id"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="target_id"></param>
        public void RequestMailNotify(int message_id, string subject, string body, uint target_id)
        {
            OnRequestMailNotify(message_id, subject, body, target_id);
        }

        public void BroadcastMessage(string sender, string title, string message)
        {
            OnBroadcastMessage?.Invoke(sender, title, message);
        }
    }
}
