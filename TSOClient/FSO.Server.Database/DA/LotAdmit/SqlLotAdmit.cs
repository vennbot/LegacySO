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
using System.Collections.Generic;
using System.Linq;

namespace FSO.Server.Database.DA.LotAdmit
{
    public class SqlLotAdmit : AbstractSqlDA, ILotAdmit
    {
        public SqlLotAdmit(ISqlContext context) : base(context)
        {
        }

        public void Create(DbLotAdmit bookmark)
        {
            Context.Connection.Execute("INSERT INTO fso_lot_admit (lot_id, avatar_id, admit_type) " +
                                        " VALUES (@lot_id, @avatar_id, @admit_type)"
                                        , bookmark);
        }

        public bool Delete(DbLotAdmit bookmark)
        {
            return Context.Connection.Execute("DELETE FROM fso_lot_admit WHERE lot_id = @lot_id AND admit_type = @admit_type AND avatar_id = @avatar_id", bookmark) > 0;
        }

        public List<DbLotAdmit> GetLotInfo(int lot_id)
        {
            return Context.Connection.Query<DbLotAdmit>("SELECT * FROM fso_lot_admit WHERE lot_id = @lot_id", new { lot_id = lot_id }).ToList();
        }

        public List<uint> GetLotAdmitDeny(int lot_id, byte admit_type)
        {
            return Context.Connection.Query<uint>("SELECT avatar_id FROM fso_lot_admit WHERE lot_id = @lot_id AND admit_type = @admit_type", new { lot_id = lot_id, admit_type = admit_type }).ToList();
        }
    }
}
