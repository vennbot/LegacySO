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
using System;
using System.Collections.Generic;

namespace FSO.Server.Database.DA.LotTop100
{
    public interface ILotTop100
    {
        void Replace(IEnumerable<DbLotTop100> values);
        IEnumerable<DbLotTop100> All();
        IEnumerable<DbLotTop100> GetAllByShard(int shard_id);
        IEnumerable<DbLotTop100> GetByCategory(int shard_id, LotCategory category);
        bool Calculate(DateTime date, int shard_id);
    }
}
