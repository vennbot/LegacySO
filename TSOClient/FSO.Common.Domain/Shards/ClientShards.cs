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
using System.Linq;
using FSO.Server.Protocol.CitySelector;

namespace FSO.Common.Domain.Shards
{
    public class ClientShards : IShardsDomain
    {
        public int? CurrentShard { get; set; }

        public List<ShardStatusItem> All
        {
            get; set;
        } = new List<ShardStatusItem>();

        public ShardStatusItem GetById(int id)
        {
            return All.FirstOrDefault(x => x.Id  == id);
        }

        public ShardStatusItem GetByName(string name)
        {
            return All.FirstOrDefault(x => x.Name == name);
        }
    }
}
