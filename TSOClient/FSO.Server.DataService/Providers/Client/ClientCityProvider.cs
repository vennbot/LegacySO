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
using FSO.Common.DataService.Framework;
using FSO.Common.DataService.Model;
using System.Collections.Immutable;

namespace FSO.Common.DataService.Providers.Client
{
    public class ClientCityProvider : ReceiveOnlyServiceProvider<uint, City>
    {
        protected override City CreateInstance(uint key)
        {
            var city = new City
            {
                City_NeighborhoodsVec = ImmutableList.Create<uint>(),
                City_OnlineLotVector = ImmutableList.Create<bool>(),
                City_ReservedLotInfo = ImmutableDictionary.Create<uint, bool>(),
                City_ReservedLotVector = ImmutableList.Create<bool>(),
                City_SpotlightsVector = ImmutableList.Create<uint>(),
                City_Top100ListIDs = ImmutableList.Create<uint>(),
                City_TopTenNeighborhoodsVector = ImmutableList.Create<uint>()
            };

            return city;
        }
    }
}
