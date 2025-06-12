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

namespace FSO.Server.Database.DA.Inbox
{
    public class DbInboxMsg
    {
        public int message_id { get; set; }
        public uint sender_id { get; set; }
        public uint target_id { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string sender_name { get; set; }
        public DateTime time { get; set; }
        public int msg_type { get; set; }
        public int msg_subtype { get; set; }
        public int read_state { get; set; }
        public int? reply_id { get; set; }
    }
}
