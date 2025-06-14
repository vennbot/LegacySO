
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

namespace FSO.Server.DataService.Providers.Client
{
    public class ClientAvatarProvider : ReceiveOnlyServiceProvider<uint, Avatar>
    {
        protected override Avatar CreateInstance(uint key)
        {
            var avatar = base.CreateInstance(key);
            avatar.RequestDefaultData = true;

            //TODO: Use the string tables
            avatar.Avatar_Id = key;
            avatar.Avatar_Name = "Retrieving...";
            avatar.Avatar_Description = "Retrieving...";

            //mab000_xy__proxy
            avatar.Avatar_Appearance.AvatarAppearance_BodyOutfitID = 2525440770061;
            //mah000_proxy
            avatar.Avatar_Appearance.AvatarAppearance_HeadOutfitID = 3985729650701;
            return avatar;
        }
    }
}
