
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
using Microsoft.Xna.Framework.Input;

namespace FSO.Common.Utils
{
    public struct CursorGroup
    {
        public Texture2D Texture;
        public MouseCursor MouseCursor;
        public Point Point;

        public CursorGroup(MouseCursor mouseCursor)
        {
            Texture = null;
            MouseCursor = mouseCursor;
            Point = new Point();
        }

        public CursorGroup(Texture2D texture, Point point)
        {
            Texture = texture;
            MouseCursor = MouseCursor.FromTexture2D(texture, point.X, point.Y);
            Point = point;
        }
    }
}
