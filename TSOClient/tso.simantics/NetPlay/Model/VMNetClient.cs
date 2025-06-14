
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
using System.Collections.Generic;

namespace FSO.SimAntics.NetPlay.Model
{
    /// <summary>
    /// Internal client repesentation for VMServerDriver. Keeps some state and thread safe message queuing.
    /// </summary>
    public class VMNetClient
    {
        public uint PersistID;
        public string RemoteIP;
        public VMNetAvatarPersistState AvatarState; //initial... obviously this can change while the lot is running.
        public bool HadAvatar;
        public int InactivityTicks;
        public object NetHandle;
        public string FatalDCMessage;

        private Queue<VMNetMessage> Messages = new Queue<VMNetMessage>();

        internal Queue<VMNetMessage> GetMessages()
        {
            lock (this)
            {
                var last = Messages;
                Messages = new Queue<VMNetMessage>();
                return last;
            }
        }

        internal void SubmitMessage(VMNetMessage msg)
        {
            lock (this)
            {
                Messages.Enqueue(msg);
            }
        }
    }
}
