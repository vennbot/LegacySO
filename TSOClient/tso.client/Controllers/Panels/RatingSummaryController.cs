
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
using FSO.Common.DataService;
using FSO.Common.DataService.Model;
using FSO.Common.Utils;
using FSO.Server.DataService.Model;
using System;

namespace FSO.Client.Controllers.Panels
{
    public interface IUIAbstractRating
    {
        Binding<MayorRating> CurrentRating { get; set; }
        uint HalfStars { get; set; }
    }

    public class RatingSummaryController : IDisposable
    {
        private Network.Network Network;
        private IClientDataService DataService;
        private IUIAbstractRating View;
        private BookmarkType CurrentType = BookmarkType.AVATAR;
        private uint RatingID = uint.MaxValue;

        public RatingSummaryController(IUIAbstractRating view, IClientDataService dataService, Network.Network network)
        {
            this.Network = network;
            this.DataService = dataService;
            this.View = view;
        }

        public void SetRating(uint ratingID)
        {
            if (ratingID == RatingID) return;
            if (ratingID == 0)
            {
                View.HalfStars = uint.MaxValue;
            }
            else
            {
                DataService.Request(MaskedStruct.Rating_User, ratingID).ContinueWith(x =>
                {
                    View.CurrentRating.Value = (x.Result as MayorRating);
                });
            }
            RatingID = ratingID;
        }

        public void Dispose()
        {
            View.CurrentRating.Dispose();
        }
    }
}
