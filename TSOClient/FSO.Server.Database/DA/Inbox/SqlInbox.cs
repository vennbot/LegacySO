
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
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FSO.Server.Database.DA.Inbox
{
    public class SqlInbox : AbstractSqlDA, IInbox
    {
        public SqlInbox(ISqlContext context) : base(context){
        }

        public int CreateMessage(DbInboxMsg msg)
        {
            var result = Context.Connection.Query<int>("INSERT INTO fso_inbox (sender_id, target_id, subject, " +
                                    "body, sender_name, time, msg_type, msg_subtype, read_state) " +
                                    " VALUES (@sender_id, @target_id, @subject, @body, @sender_name, " +
                                    " @time, @msg_type, @msg_subtype, @read_state); SELECT LAST_INSERT_ID();", msg).First();
            return result;
        }

        public bool DeleteMessage(int msgID)
        {
            return Context.Connection.Execute("DELETE FROM fso_inbox WHERE message_id = @id", new { id = msgID }) > 0;
        }

        public bool DeleteMessage(int msgID, uint avatarID)
        {
            return Context.Connection.Execute("DELETE FROM fso_inbox WHERE message_id = @id AND target_id = @aid", new { id = msgID, aid = avatarID }) > 0;
        }

        public DbInboxMsg Get(int msgID)
        {
            return Context.Connection.Query<DbInboxMsg>("SELECT * FROM fso_inbox WHERE message_id = @id", new { id = msgID }).FirstOrDefault();
        }

        public List<DbInboxMsg> GetMessages(uint avatarID)
        {
            return Context.Connection.Query<DbInboxMsg>("SELECT * FROM fso_inbox WHERE target_id = @id", new { id = avatarID }).ToList();
        }

        public List<DbInboxMsg> GetMessagesAfter(uint avatarID, DateTime after)
        {
            return Context.Connection.Query<DbInboxMsg>("SELECT * FROM fso_inbox WHERE target_id = @id AND time > @after", new { id = avatarID, after = after }).ToList();
        }
    }
}
