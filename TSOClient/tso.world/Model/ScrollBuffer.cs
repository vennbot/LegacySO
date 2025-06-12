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
using System;

namespace FSO.LotView.Model
{
    public class ScrollBuffer
    {
        public static int BUFFER_PADDING = 512;

        public Vector2 GetScrollIncrement(Vector2 pxOffset, WorldState state)
        {
            var scrollSize = BUFFER_PADDING / state.PreciseZoom;
            return new Vector2((float)Math.Floor(pxOffset.X / scrollSize) * scrollSize, (float)Math.Floor(pxOffset.Y / scrollSize) * scrollSize);
        }

        public Texture2D Pixel;
        public Texture2D Depth;
        public Vector2 PxOffset;
        public Vector3 WorldPosition;

        public ScrollBuffer(Texture2D pixel, Texture2D depth, Vector2 px, Vector3 world)
        {
            Pixel = pixel;
            Depth = depth;
            PxOffset = px;
            WorldPosition = world;
        }
    }
}
