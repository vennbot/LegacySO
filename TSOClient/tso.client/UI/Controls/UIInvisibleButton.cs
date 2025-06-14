
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FSO.Common.Rendering.Framework.IO;

namespace FSO.Client.UI.Controls
{
    /// <summary>
    /// A simple invisible rectangular button.
    /// </summary>
    public class UIInvisibleButton : UIButton
    {
        public UIInvisibleButton(int width, int height, Texture2D invisibleTexture)
        {
            Texture = invisibleTexture;

            ImageStates = 1;

            ClickHandler =
                ListenForMouse(new Rectangle(0, 0, width, height), new UIMouseEvent(OnMouseEvent));

            ActivateTooltip();
        }
    }
}
