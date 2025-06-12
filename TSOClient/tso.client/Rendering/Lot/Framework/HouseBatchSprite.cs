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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TSOClient.Code.Rendering.Lot.Framework
{
    public class HouseBatchSprite
    {
        public HouseBatchRenderMode RenderMode { get; set; }
        public Texture2D Pixel { get; set; }
        public Texture2D Depth { get; set; }
        public Vector2 TilePosition { get; set; }

        public Rectangle SrcRect { get; set; }
        public Rectangle DestRect { get; set; }

        //For internal use, do not set this
        public int DrawOrder { get; set; }
    }
}
