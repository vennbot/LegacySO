
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

namespace FSO.Server.Database.DA.GlobalCooldowns
{
    public class SqlGlobalCooldowns : AbstractSqlDA, IGlobalCooldowns
    {
        public SqlGlobalCooldowns(ISqlContext context) : base(context)
        {
        }

        public DbGlobalCooldowns Get(uint objguid, uint avatarOrUserid, bool useAccount, uint category)
        {
            if (useAccount)
                return Context.Connection.Query<DbGlobalCooldowns>("SELECT * FROM fso_global_cooldowns WHERE object_guid = @guid AND " +
                    "user_id = @id AND category = @category", new { guid = objguid, id = avatarOrUserid, category = category }).FirstOrDefault();
            else
                return Context.Connection.Query<DbGlobalCooldowns>("SELECT * FROM fso_global_cooldowns WHERE object_guid = @guid AND " +
                    "avatar_id = @id AND category = @category", new { guid = objguid, id = avatarOrUserid, category = category }).FirstOrDefault();
        }

        public List<DbGlobalCooldowns> GetAllByObj(uint objguid)
        {
            return Context.Connection.Query<DbGlobalCooldowns>("SELECT * FROM fso_global_cooldowns WHERE object_guid = @guid", new { guid = objguid }).ToList();
        }

        public List<DbGlobalCooldowns> GetAllByAvatar(uint avatarid)
        {
            return Context.Connection.Query<DbGlobalCooldowns>("SELECT * FROM fso_global_cooldowns WHERE avatar_id = @avatarid", new { avatarid = avatarid }).ToList();
        }
        
        public List<DbGlobalCooldowns> GetAllByObjectAndAvatar(uint objguid, uint avatarid)
        {
            return Context.Connection.Query<DbGlobalCooldowns>("SELECT * FROM fso_global_cooldowns WHERE object_guid = @guid AND " +
                "avatar_id = @avatarid", new { guid = objguid, avatarid = avatarid }).ToList();
        }
        public bool Create(DbGlobalCooldowns newCooldown)
        {
            return Context.Connection.Execute("INSERT INTO fso_global_cooldowns (object_guid, avatar_id, user_id, category, expiry) " +
                "VALUES (@object_guid, @avatar_id, @user_id, @category, @expiry)", newCooldown) > 0;
        }
        public bool Update(DbGlobalCooldowns updatedCooldown)
        {
            return Context.Connection.Execute("UPDATE fso_global_cooldowns SET expiry = @expiry WHERE object_guid = @object_guid AND " +
                "avatar_id = @avatar_id AND user_id = @user_id AND category = @category", updatedCooldown) > 0;
        }
    }
}
