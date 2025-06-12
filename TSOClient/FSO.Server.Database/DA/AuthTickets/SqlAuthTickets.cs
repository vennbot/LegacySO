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
using System.Linq;
using Dapper;

namespace FSO.Server.Database.DA.AuthTickets
{
    public class SqlAuthTickets : AbstractSqlDA, IAuthTickets
    {
        public SqlAuthTickets(ISqlContext context) : base(context)
        {
        }

        public void Create(AuthTicket ticket)
        {
            Context.Connection.Execute("INSERT INTO fso_auth_tickets VALUES (@ticket_id, @user_id, @date, @ip)", ticket);
        }

        public void Delete(string id)
        {
            Context.Connection.Execute("DELETE FROM fso_auth_tickets WHERE ticket_id = @ticket_id", new { ticket_id = id });
        }

        public AuthTicket Get(string id)
        {
            return 
                Context.Connection.Query<AuthTicket>("SELECT * FROM fso_auth_tickets WHERE ticket_id = @ticket_id", new { ticket_id = id }).FirstOrDefault();
        }

        public void Purge(uint time)
        {
            Context.Connection.Execute("DELETE FROM fso_auth_tickets WHERE date < @time", new { time = time });
        }
    }
}
