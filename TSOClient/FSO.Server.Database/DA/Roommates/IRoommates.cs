
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

namespace FSO.Server.Database.DA.Roommates
{
    public interface IRoommates
    {
        bool Create(DbRoommate roomie);
        bool CreateOrUpdate(DbRoommate roomie);
        DbRoommate Get(uint avatar_id, int lot_id);
        List<DbRoommate> GetAvatarsLots(uint avatar_id);
        List<DbRoommate> GetLotRoommates(int lot_id);
        uint RemoveRoommate(uint avatar_id, int lot_id);
        bool DeclineRoommateRequest(uint avatar_id, int lot_id);
        bool AcceptRoommateRequest(uint avatar_id, int lot_id);
        bool UpdatePermissionsLevel(uint avatar_id, int lot_id, byte level);
    }
}
