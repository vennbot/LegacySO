
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
namespace FSO.Client.UI.Panels.EODs
{
    public class EODLiveModeOpt
    {
        public EODHeight Height;
        public EODLength Length;
        public EODTextTips Tips;
        public EODTimer Timer;
        public byte Buttons; //0,1,2. graphics for 3 are present but currently unused.
        public bool Expandable; //enables "double panel" mode. can only be used with tall EOD.
        public bool Expanded;
        
        public EODLength TopPanelLength;
        public byte TopPanelButtons;
    }

    public enum EODHeight
    {
        Normal,
        Tall,
        TallTall,
        ExtraTall,
        Trade
    }

    public enum EODLength
    {
        Short,
        Medium,
        Full,
        None
    }

    public enum EODTextTips
    {
        None,
        Short, //has straight variation for short EOD, onlinejobs?
        Long,
    }

    public enum EODTimer
    {
        None,
        Normal,
        Straight //used for OnlineJobs. technically not an EOD??
    }
}
