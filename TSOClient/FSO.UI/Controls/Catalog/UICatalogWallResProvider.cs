
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
using Microsoft.Xna.Framework.Graphics;

namespace FSO.Client.UI.Controls.Catalog
{
    public class UICatalogWallResProvider : UICatalogResProvider
    {

        public override Texture2D GetIcon(ulong id)
        {
            return Content.Content.Get().WorldWalls.GetWallStyleIcon((ushort)id).GetTexture(GameFacade.GraphicsDevice);
        }

        public override string GetName(ulong id)
        {
            return Content.Content.Get().WorldWalls.GetWallStyle(id).Name;
        }

        public override string GetDescription(ulong id)
        {
            return Content.Content.Get().WorldWalls.GetWallStyle(id).Description;
        }

        public override int GetPrice(ulong id)
        {
            return Content.Content.Get().WorldWalls.GetWallStyle(id).Price;
        }
    }
}
