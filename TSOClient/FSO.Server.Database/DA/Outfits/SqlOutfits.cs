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
using System;
using System.Collections.Generic;
using System.Linq;

namespace FSO.Server.Database.DA.Outfits
{
    public class SqlOutfits : AbstractSqlDA, IOutfits
    {
        public SqlOutfits(ISqlContext context) : base(context){
        }

        public uint Create(DbOutfit outfit)
        {
            try {
                return (uint)Context.Connection.Query<int>("INSERT INTO fso_outfits (avatar_owner, object_owner, asset_id, sale_price, purchase_price, outfit_type, outfit_source) " +
                                            " VALUES (@avatar_owner, @object_owner, @asset_id, @sale_price, @purchase_price, @outfit_type, @outfit_source); " +
                                            " SELECT LAST_INSERT_ID();"
                                            , new {
                                                avatar_owner = outfit.avatar_owner,
                                                object_owner = outfit.object_owner,
                                                asset_id = outfit.asset_id,
                                                sale_price = outfit.sale_price,
                                                purchase_price = outfit.purchase_price,
                                                outfit_type = outfit.outfit_type,
                                                outfit_source = outfit.outfit_source.ToString()
                                            }).First();
            }catch(Exception ex){
                return uint.MaxValue;
            }
        }

        public bool ChangeOwner(uint outfit_id, uint object_owner, uint new_avatar_owner)
        {
            return Context.Connection.Execute("UPDATE fso_outfits SET avatar_owner = @avatar_owner, object_owner = NULL WHERE outfit_id = @outfit_id and object_owner = @object_owner", new { outfit_id = outfit_id, object_owner = object_owner, avatar_owner = new_avatar_owner }) > 0;
        }

        public bool UpdatePrice(uint outfit_id, uint object_owner, int new_price)
        {
            return Context.Connection.Execute("UPDATE fso_outfits SET sale_price = @sale_price WHERE outfit_id = @outfit_id AND object_owner = @object_owner", new { outfit_id = outfit_id, object_owner = object_owner, sale_price = new_price }) > 0;
        }

        public bool DeleteFromObject(uint outfit_id, uint object_id)
        {
            return Context.Connection.Execute("DELETE FROM fso_outfits WHERE outfit_id = @outfit_id AND object_owner = @object_owner", new { outfit_id = outfit_id, object_owner = object_id }) > 0;
        }
        
        public bool DeleteFromAvatar(uint outfit_id, uint avatar_id)
        {
            return Context.Connection.Execute("DELETE FROM fso_outfits WHERE outfit_id = @outfit_id AND avatar_owner = @avatar_owner", new { outfit_id = outfit_id, avatar_owner = avatar_id }) > 0;
        }

        public List<DbOutfit> GetByAvatarId(uint avatar_id)
        {
            return Context.Connection.Query<DbOutfit>("SELECT * FROM fso_outfits WHERE avatar_owner = @avatar_id", new { avatar_id = avatar_id }).ToList();
        }

        public List<DbOutfit> GetByObjectId(uint object_id)
        {
            return Context.Connection.Query<DbOutfit>("SELECT * FROM fso_outfits WHERE object_owner = @object_id", new { object_id = object_id }).ToList();
        }

    }
}
