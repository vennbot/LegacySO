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

namespace FSO.Server.Database.DA.Neighborhoods
{
    public interface INeighborhoods
    {
        List<DbNeighborhood> All(int shard_id);
        DbNeighborhood Get(uint neighborhood_id);
        DbNeighborhood GetByMayor(uint mayor_id);
        DbNeighborhood GetByLocation(uint location);
        int DeleteMissing(int shard_id, List<string> AllGUIDs);
        int UpdateFromJSON(DbNeighborhood update);
        int AddNhood(DbNeighborhood update);
        void UpdateDescription(uint neighborhood_id, string description);
        void UpdateMayor(uint neigh_id, uint? mayor_id);
        void UpdateTownHall(uint neigh_id, uint? lot_id);
        void UpdateCycle(uint neigh_id, uint? cycle_id);
        void UpdateName(uint neighborhood_id, string name);
        void UpdateFlag(uint neighborhood_id, uint flag);

        DbNhoodBan GetNhoodBan(uint user_id);
        bool AddNhoodBan(DbNhoodBan ban);

        List<DbNeighborhood> SearchExact(int shard_id, string name, int limit);
        List<DbNeighborhood> SearchWildcard(int shard_id, string name, int limit);
    }
}
