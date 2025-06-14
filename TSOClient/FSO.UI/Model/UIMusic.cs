
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
namespace FSO.Client.UI.Model
{
    public sealed class UIMusic //hey guys, it's totally an enum!
    {
        public static readonly string LoadLoop = "bkground_load";
        public static readonly string SAS = "bkground_selectasim";
        public static readonly string CAS = "bkground_createasim";
        public static readonly string Map = "bkground_nhoodpluslot";
        public static readonly string None = "bkground_fade";

        public static readonly string Buy = "bkground_buy1";
        public static readonly string Build = "bkground_build";
        public static readonly string Nhood = "bkground_nhood1";
        public static readonly string Downtown = "station_dtnhood";
        public static readonly string Vacation = "station_vacation";
        public static readonly string Unleashed = "station_unleashed";
        public static readonly string Superstar = "station_superstar";
        public static readonly string SuperstarTransition = "music_superstar_transition";
        public static readonly string Magictown = "music_magictown";
        public static readonly string MagictownCredits = "music_magictown_credits";
        public static readonly string MagictownBuild = "music_magictown_build";
        public static readonly string MagictownBuy = "music_magictown_buy";
    }
}
