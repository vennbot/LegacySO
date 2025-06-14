
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

namespace FSO.Server.Database.DA.Bulletin
{
    public interface IBulletinPosts
    {
        DbBulletinPost Get(uint bulletin_id);
        int CountPosts(uint neighborhood_id, uint timeAfter);
        uint LastPostID(uint neighborhood_id);
        DbBulletinPost LastUserPost(uint user_id, uint neighborhood_id);
        List<DbBulletinPost> GetByNhoodId(uint neighborhood_id, uint timeAfter);
        uint Create(DbBulletinPost bulletin);
        bool Delete(uint bulletin_id);
        bool SoftDelete(uint bulletin_id);
        bool SetTypeFlag(uint bulletin_id, DbBulletinType type, int flag);
    }
}
