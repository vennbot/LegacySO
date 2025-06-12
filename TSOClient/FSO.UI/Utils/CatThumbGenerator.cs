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
using FSO.Client;
using FSO.Common.Utils;
using FSO.LotView.Components;
using FSO.SimAntics;
using FSO.SimAntics.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FSO.UI.Utils
{
    public static class CatThumbGenerator
    {
        public static Texture2D GenerateThumb(VMMultitileGroup obj, VM vm)
        {
            var gd = GameFacade.GraphicsDevice;
            var objects = obj.Objects;
            ObjectComponent[] objComps = new ObjectComponent[objects.Count];
            for (int i = 0; i < objects.Count; i++)
            {
                objComps[i] = (ObjectComponent)objects[i].WorldUI;
            }
            var thumb = vm.Context.World.GetObjectThumb(objComps, obj.GetBasePositions(), GameFacade.GraphicsDevice);

            var data = new Color[thumb.Width * thumb.Height];
            thumb.GetData(data);
            thumb.Dispose();
            var newAgain = new Texture2D(GameFacade.GraphicsDevice, thumb.Width, thumb.Height, true, SurfaceFormat.Color);
            TextureUtils.UploadWithMips(newAgain, GameFacade.GraphicsDevice, data);

            var sb = new SpriteBatch(GameFacade.GraphicsDevice);
            var result = new RenderTarget2D(GameFacade.GraphicsDevice, 74, 37);

            var oldRts = gd.GetRenderTargets();
            gd.SetRenderTarget(result);
            gd.Clear(Color.Black);
            sb.Begin(blendState: BlendState.AlphaBlend);
            var minScale = Math.Min(37f/newAgain.Width, 37f/newAgain.Height);
            if (minScale > 1) minScale = 1;
            var rect = new Rectangle(
                (int)(newAgain.Width * minScale / -2 + 18),
                (int)(newAgain.Height * minScale / -2 + 19),
                (int)(minScale * newAgain.Width),
                (int)(minScale * newAgain.Height));

            var px = TextureGenerator.GetPxWhite(GameFacade.GraphicsDevice);
            sb.Draw(px, new Rectangle(0, 0, 37, 37), new Color(56, 88, 120));
            sb.Draw(px, new Rectangle(37, 0, 37, 37), new Color(184, 212, 240));

            sb.Draw(newAgain, rect, Color.White);
            rect.Offset(37, 0);
            sb.Draw(newAgain, rect, Color.White);
            sb.End();

            gd.SetRenderTargets(oldRts);
            newAgain.Dispose();
            return result;
        }
    }
}
