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
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace FSO.Server.Database.DA.Shards
{
    public class SqlShards : AbstractSqlDA, IShards
    {
        public SqlShards(ISqlContext context) : base(context) {
        }

        public List<Shard> All()
        {
            return Context.Connection.Query<Shard>("SELECT * FROM fso_shards").ToList();
        }

        public void CreateTicket(ShardTicket ticket)
        {
            Context.Connection.Execute("INSERT INTO fso_shard_tickets VALUES (@ticket_id, @user_id, @date, @ip, @avatar_id)", ticket);
        }

        public void DeleteTicket(string id)
        {
            Context.Connection.Execute("DELETE FROM fso_shard_tickets WHERE ticket_id = @ticket_id", new { ticket_id = id });
        }

        public ShardTicket GetTicket(string id)
        {
            return
                Context.Connection.Query<ShardTicket>("SELECT * FROM fso_shard_tickets WHERE ticket_id = @ticket_id", new { ticket_id = id }).FirstOrDefault();
        }

        public void PurgeTickets(uint time)
        {
            Context.Connection.Query("DELETE FROM fso_shard_tickets WHERE date < @time", new { time = time });
        }

        public void UpdateStatus(int shard_id, string internal_host, string public_host, string name, string number, int? update_id)
        {
            Context.Connection.Query("UPDATE fso_shards SET internal_host = @internal_host, public_host = @public_host, version_name = @version_name, version_number = @version_number, update_id = @update_id WHERE shard_id = @shard_id", new
            {
                internal_host,
                public_host,
                version_name = name,
                version_number = number,
                update_id,
                shard_id
            });
        }
    }
}
