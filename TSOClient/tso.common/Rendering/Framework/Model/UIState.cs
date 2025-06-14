
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

namespace FSO.Common.Rendering.Framework.Model
{
    public class UIState
    {
        public int Width;
        public int Height;
        public UITooltipProperties TooltipProperties = new UITooltipProperties();
        public string Tooltip;
    }

    public class UITooltipProperties
    {
        public float Opacity;
        public Vector2 Position;
        public bool Show;
        public Color Color = Color.Black;
        public bool UpdateDead;
    }
}
