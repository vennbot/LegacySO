// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
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
using FSO.Common.Rendering.Framework.IO;
using FSO.Common.Rendering.Framework.Model;

namespace FSO.Client.UI.Controls
{
    /// <summary>
    /// Just blocks and sinks mouse events
    /// </summary>
    public class UIBlocker : UIElement
    {
        private UIMouseEventRef MouseEvt;
        public UIMouseEvent OnMouseEvt;

        public UIBlocker()
        {
            MouseEvt = this.ListenForMouse(new Microsoft.Xna.Framework.Rectangle(0, 0, 10, 10), OnMouse);
            SetSize(GlobalSettings.Default.GraphicsWidth, GlobalSettings.Default.GraphicsHeight);
        }

        public UIBlocker(int width, int height)
        {
            MouseEvt = this.ListenForMouse(new Microsoft.Xna.Framework.Rectangle(0, 0, 10, 10), OnMouse);
            SetSize(width, height);
        }

        private void OnMouse(UIMouseEventType type, UpdateState state)
        {
            if (OnMouseEvt != null) OnMouseEvt(type, state);
        }

        public void SetSize(int width, int height)
        {
            MouseEvt.Region.Width = width;
            MouseEvt.Region.Height = height;
        }

        public override void Draw(UISpriteBatch batch)
        {
        }
    }
}
