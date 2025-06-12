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
using Dapper;
using FSO.Server.Common;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSO.Server.Database.DA.Relationships
{
    public class SqlRelationships : AbstractSqlDA, IRelationships
    {

        public SqlRelationships(ISqlContext context) : base(context)
        {

        }
        public int Delete(uint entity_id)
        {
            return Context.Connection.ExecuteScalar<int>(
                "DELETE FROM fso_relationships WHERE from_id = @entity_id OR to_id = @entity_id",
                new { entity_id = entity_id }
            );
        }

        public List<DbRelationship> GetBidirectional(uint entity_id)
        {
            return Context.Connection.Query<DbRelationship>(
                "SELECT * FROM fso_relationships WHERE (from_id = @entity_id AND to_id < 16777216) OR (to_id = @entity_id AND from_id < 16777216)",
                new { entity_id = entity_id }
            ).ToList();
        }

        public List<DbRelationship> GetOutgoing(uint entity_id)
        {
            return Context.Connection.Query<DbRelationship>(
                "SELECT * FROM fso_relationships WHERE from_id = @entity_id",
                new { entity_id = entity_id }
            ).ToList();
        }

        public int UpdateMany(List<DbRelationship> entries)
        {
            var date = Epoch.Now;
            var conn = (MySqlConnection)Context.Connection;
            int rows;
            using (MySqlCommand cmd = new MySqlCommand("", conn))
            {
                try
                {
                    StringBuilder sCommand = new StringBuilder("INSERT INTO fso_relationships (from_id, to_id, value, `index`, `date`) VALUES ");

                    bool first = true;
                    foreach (var item in entries)
                    {
                        if (!first) sCommand.Append(",");
                        first = false;
                        sCommand.Append("(");
                        sCommand.Append(item.from_id);
                        sCommand.Append(",");
                        sCommand.Append(item.to_id);
                        sCommand.Append(",");
                        sCommand.Append(item.value);
                        sCommand.Append(",");
                        sCommand.Append(item.index);
                        sCommand.Append(",");
                        sCommand.Append(date);
                        sCommand.Append(")");
                    }
                    sCommand.Append(" ON DUPLICATE KEY UPDATE value = VALUES(`value`); ");

                    cmd.CommandTimeout = 300;
                    cmd.CommandText = sCommand.ToString();
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    return -1;
                }
                return rows;
            }
        }
    }
}
