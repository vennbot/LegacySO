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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FSO.Common.Rendering
{
    public class TextureInfo
    {
        public Vector2 UVScale;
        public Point Size;
        public Point Diff;

        public TextureInfo() { }

        public TextureInfo(Texture2D tex, int width, int height)
        {
            Size = new Point(width, height);
            Diff = new Point(tex.Width, tex.Height) - Size;
            UVScale = Size.ToVector2() / new Vector2(tex.Width, tex.Height);
        }
    }
}
