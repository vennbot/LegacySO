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

namespace FSO.Server.Database.DA.Bookmarks
{
    public class SqlBookmarks : AbstractSqlDA, IBookmarks
    {
        public SqlBookmarks(ISqlContext context) : base(context)
        {
        }

        public void Create(DbBookmark bookmark)
        {
            Context.Connection.Execute("INSERT INTO fso_bookmarks (avatar_id, type, target_id) " +
                                        " VALUES (@avatar_id, @type, @target_id)"
                                        , bookmark);
        }

        public bool Delete(DbBookmark bookmark)
        {
            return Context.Connection.Execute("DELETE FROM fso_bookmarks WHERE avatar_id = @avatar_id AND type = @type AND target_id = @target_id", bookmark) > 0;
        }

        public List<DbBookmark> GetByAvatarId(uint avatar_id)
        {
            return Context.Connection.Query<DbBookmark>("SELECT * FROM fso_bookmarks WHERE avatar_id = @avatar_id", new { avatar_id = avatar_id }).ToList();
        }

        public List<uint> GetAvatarIgnore(uint avatar_id)
        {
            return Context.Connection.Query<uint>("SELECT target_id FROM fso_bookmarks WHERE avatar_id = @avatar_id AND type = 5", new { avatar_id = avatar_id }).ToList();
        }
    }
}
