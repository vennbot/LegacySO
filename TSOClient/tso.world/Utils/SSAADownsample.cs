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

namespace FSO.LotView.Utils
{
    public static class SSAADownsample
    {
        public static void Draw(GraphicsDevice gd, RenderTarget2D targ)
        {
            var effect = WorldContent.SSAA;
            gd.BlendState = BlendState.Opaque;
            effect.CurrentTechnique = effect.Techniques[0];
            effect.Parameters["SSAASize"].SetValue(new Vector2(1f / targ.Width, 1f / targ.Height));
            effect.Parameters["tex"].SetValue(targ);
            effect.CurrentTechnique.Passes[0].Apply();

            gd.SetVertexBuffer(WorldContent.GetTextureVerts(gd));
            gd.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
        }
    }
}
