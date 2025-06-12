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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FSO.Client.UI.Controls;
using FSO.Client.UI.Framework;

namespace FSO.Client.UI.Model
{
    public class SlicedTextureRef : ITextureRef
    {
        public Texture2D Texture;
        public Rectangle Margins;

        private NineSliceMargins NineSlice;

        public SlicedTextureRef(Texture2D texture, Rectangle margins)
        {
            this.Texture = texture;
            this.Margins = margins;

            NineSlice = new NineSliceMargins {
                Left = margins.Left,
                Top = margins.Top,
                Right = margins.Width,
                Bottom = margins.Height
            };
            NineSlice.CalculateOrigins(texture);
        }

        public void Draw(SpriteBatch SBatch, UIElement element, float x, float y, float width, float height)
        {
            //TODO: Cache scales for various sizes?
            NineSlice.CalculateScales(width, height);
            NineSlice.DrawOntoPosition(SBatch, element, Texture, width, height, new Vector2(x, y));
        }
    }


    public interface ITextureRef
    {
        void Draw(SpriteBatch SBatch, UIElement element, float x, float y, float width, float height);
    }
}
