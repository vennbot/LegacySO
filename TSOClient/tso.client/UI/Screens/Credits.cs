
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
using FSO.Client.UI.Framework;
using Microsoft.Xna.Framework.Graphics;
using FSO.Client.UI.Controls;

namespace FSO.Client.UI.Screens
{
    public class Credits : GameScreen
    {
        public Texture2D BackgroundImage { get; set; }
        public UIButton BackButton { get; set; }
        public UIButton OkButton { get; set; }

        public Credits()
        {
            var ui = this.RenderScript("credits.uis");

            this.X = (float)((double)(ScreenWidth - 800)) / 2;
            this.Y = (float)((double)(ScreenHeight - 600)) / 2;

            this.AddAt(0, new UIImage(BackgroundImage));
            this.Add(ui.Create<UIImage>("TSOLogoImage"));


            BackButton.OnButtonClick += new ButtonClickDelegate(BackButton_OnButtonClick);
            OkButton.OnButtonClick += new ButtonClickDelegate(BackButton_OnButtonClick);
        }

        void BackButton_OnButtonClick(UIElement button)
        {
            GameFacade.Screens.RemoveScreen(this);
        }
    }
}
