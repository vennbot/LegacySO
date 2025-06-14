
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
using FSO.Common.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace FSO.Files.RC.Utils
{
    public class DepthTreatment
    {
        public static Effect SpriteEffect;
        public static SpriteBatch Batch;
        
        public DepthTreatment()
        {
        }

        public static void EnsureBatch(GraphicsDevice gd)
        {
            if (Batch == null)
            {
                Batch = new SpriteBatch(gd);
            }
        }

        public static float[] DequantizeDepth(GraphicsDevice gd, Texture2D depthIn)
        {
            var wait = new AutoResetEvent(false);
            float[] data = null;
            GameThread.InUpdate(() =>
            {
                var targetPrep = new RenderTarget2D(gd, depthIn.Width, depthIn.Height, false, SurfaceFormat.Vector4, DepthFormat.None);
                var target = new RenderTarget2D(gd, depthIn.Width, depthIn.Height, false, SurfaceFormat.Single, DepthFormat.None);
                gd.SetRenderTarget(targetPrep);

                var effect = SpriteEffect;
                EnsureBatch(gd);

                effect.CurrentTechnique = effect.Techniques["DerivativeDepth"];
                effect.Parameters["pixelSize"].SetValue(new Vector2(1f / depthIn.Width, 1f / depthIn.Height));

                Batch.Begin(rasterizerState: RasterizerState.CullNone, effect: effect);
                Batch.Draw(depthIn, Vector2.Zero, Color.White);
                Batch.End();

                gd.SetRenderTarget(target);

                effect.CurrentTechnique = effect.Techniques["DequantizeDepth"];

                Batch.Begin(rasterizerState: RasterizerState.CullNone, effect: effect);
                Batch.Draw(targetPrep, Vector2.Zero, Color.White);
                Batch.End();
                
                gd.SetRenderTarget(null);

                data = new float[depthIn.Width * depthIn.Height];
                target.GetData<float>(data);
                target.Dispose();
                targetPrep.Dispose();
                wait.Set();
            });
            wait.WaitOne();
            return data;
        }
    }
}
