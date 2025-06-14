
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
using FSO.Client.UI.Panels.Neighborhoods;
using FSO.Common.DataService;
using FSO.Common.DataService.Model;
using FSO.Server.DataService.Model;
using System;

namespace FSO.Client.Controllers.Panels
{
    public class RatingListController : IDisposable
    {
        private Network.Network Network;
        private IClientDataService DataService;
        private UIRatingList View;

        public RatingListController(UIRatingList view, IClientDataService dataService, Network.Network network)
        {
            this.Network = network;
            this.DataService = dataService;
            this.View = view;
        }

        public void SetAvatar(uint avatarID)
        {
            DataService.Request(MaskedStruct.MayorInfo_Avatar, avatarID).ContinueWith(x =>
            {
                View.CurrentAvatar.Value = (x.Result as Avatar);
            });
        }

        public void Dispose()
        {
            View.CurrentAvatar.Dispose();
        }
    }
}
