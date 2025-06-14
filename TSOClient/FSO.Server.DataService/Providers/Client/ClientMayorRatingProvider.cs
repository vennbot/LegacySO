
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

namespace FSO.Common.DataService.Providers.Client
{
    public class ClientMayorRatingProvider : ReceiveOnlyServiceProvider<uint, MayorRating>
    {
        protected override MayorRating CreateInstance(uint key)
        {
            var rating = base.CreateInstance(key);
            rating.Id = key;
            rating.MayorRating_Comment = "Retrieving...";
            return rating;
        }

    }
}
