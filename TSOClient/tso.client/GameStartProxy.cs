
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
using FSO.LotView;
using FSO.UI;

namespace FSO.Client
{
    /// <summary>
    /// To avoid dynamically linking monogame from Program.cs (where we have to choose the correct version for the OS),
    /// we use this mediator class.
    /// </summary>
    public class GameStartProxy : IGameStartProxy
    {
        public void Start(bool useDX)
        {
            GameFacade.DirectX = useDX;
			World.DirectX = useDX;
            TSOGame game = new TSOGame();
            game.Run();
            game.Dispose();
        }

		public void SetPath(string path)
		{
			GlobalSettings.Default.StartupPath = path;
            GlobalSettings.Default.Windowed = false;
		}


	}
}
