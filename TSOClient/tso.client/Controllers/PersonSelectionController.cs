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
using FSO.Client.Regulators;
using FSO.Client.UI.Screens;
using FSO.Common.Domain.Shards;
using FSO.Common.Utils.Cache;
using FSO.Server.Protocol.CitySelector;

namespace FSO.Client.Controllers
{
    public class PersonSelectionController
    {
        private PersonSelection View;
        private LoginRegulator Regulator;
        private IShardsDomain Shards;
        private ICache Cache;

        public PersonSelectionController(PersonSelection view, LoginRegulator regulator, IShardsDomain shards, ICache cache)
        {
            this.Shards = shards;
            this.View = view;
            this.Regulator = regulator;
            this.Cache = cache;
        }

        public void ConnectToAvatar(AvatarData avatar, bool autoJoinLot)
        {
            FSOFacade.Controller.ConnectToCity(avatar.ShardName, avatar.ID, autoJoinLot ? avatar.LotLocation : null);
        }

        public void CreateAvatar()
        {
            View.ShowCitySelector(Shards.All, (ShardStatusItem selectedShard) =>
            {
                FSOFacade.Controller.ConnectToCAS(selectedShard.Name);
            });
        }
    }
}
