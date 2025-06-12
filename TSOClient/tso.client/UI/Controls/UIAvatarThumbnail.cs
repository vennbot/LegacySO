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
using FSO.Content.Model;

namespace FSO.Client.UI.Controls
{
    public class UIAvatarThumbnail : UIElement
    {
        public ITextureRef Icon;
        private ITextureRef Background;
        private UIMouseEventRef ClickHandler;

        public UIAvatarThumbnail()
        {

        }

        public override void Draw(UISpriteBatch batch)
        {

        }
    }
}
