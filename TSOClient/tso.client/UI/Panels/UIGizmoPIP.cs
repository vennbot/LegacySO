
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
using FSO.Client.Controllers;
using FSO.Client.UI.Controls;
using FSO.Client.UI.Framework;
using FSO.Client.UI.Framework.Parser;
using Microsoft.Xna.Framework.Graphics;

namespace FSO.Client.UI.Panels
{
    public class UIGizmoPIP : UIContainer
    {
        public UISim SimBox { get; internal set; }
        private UIButton Button;

        public UIGizmoPIP()
        {
            Button = new UIButton();
            Add(Button);

            Button.OnButtonClick += Button_OnButtonClick;
        }

        private void Button_OnButtonClick(UIElement button)
        {
            //Show my sim page
            ((CoreGameScreenController)Parent.Parent.Controller).ShowMyPersonPage();
        }

        public void Initialize()
        {
            var buttonTexture = Button.Texture;

            SimBox = new UISim();
            SimBox.Position = new Microsoft.Xna.Framework.Vector2(16, 8); //new Microsoft.Xna.Framework.Vector2((buttonTexture.Width / 4) / 2.0f, buttonTexture.Height - 20.0f);
            SimBox.Avatar.BodyOutfitId = 2611340115981;
            SimBox.Avatar.HeadOutfitId = 5076651343885;
            SimBox.Size = new Microsoft.Xna.Framework.Vector2(75,140);
            SimBox.AutoRotate = true;
            this.Add(SimBox);
        }

        [UIAttribute("buttonImage")]
        public Texture2D ButtonImage
        {
            set
            {
                Button.Texture = value;
            }
        }

        [UIAttribute("maskImage")]
        public Texture2D MaskImage
        {
            set
            {
            }
        }
    }
}
