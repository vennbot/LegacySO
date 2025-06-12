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
using FSO.Common.Enum;
using System.Collections.Generic;

namespace FSO.Server.Database.DA.LotClaims
{
    public interface ILotClaims
    {
        uint? TryCreate(DbLotClaim claim);
        IEnumerable<DbLotClaim> GetAllByOwner(string owner);

        bool Claim(uint id, string previousOwner, string newOwner);
        DbLotClaim Get(uint id);
        DbLotClaim GetByLotID(int id);

        void RemoveAllByOwner(string owner);
        void Delete(uint id, string owner);
        List<DbLotStatus> AllLocations(int shardId);
        List<DbLotActive> AllActiveLots(int shardId);
        List<DbLotStatus> Top100Filter(int shard_id, LotCategory category, int limit);
        List<uint> RecentsFilter(uint avatar_id, int limit);
    }
}
