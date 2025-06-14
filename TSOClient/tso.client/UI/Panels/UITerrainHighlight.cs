
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
using FSO.Client.Rendering.City;
using FSO.Client.UI.Framework;
using FSO.Common.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FSO.Client.UI.Panels
{
    public class UITerrainHighlight
    {

        internal static void DrawLine(Texture2D Fill, Vector2 Start, Vector2 End, SpriteBatch spriteBatch, int lineWidth, Color tint) //draws a line from Start to End.
        {
            double length = Math.Sqrt(Math.Pow(End.X - Start.X, 2) + Math.Pow(End.Y - Start.Y, 2));
            float direction = (float)Math.Atan2(End.Y - Start.Y, End.X - Start.X);
            var norm = (End - Start);
            norm.Normalize();
            //Start += norm*(lineWidth/2);
            spriteBatch.Draw(Fill, new Rectangle((int)Start.X, (int)Start.Y, (int)length, lineWidth), null, tint, direction, new Vector2(0, 0.5f), SpriteEffects.None, 0); //
        }

        public static Vector2? GetEndpointFromLotId(Terrain terrain, Vector2 from, int location) // allows us to find the end point of an arrow 
        {
            var x = location >> 16;
            var y = location & 0xFFFF;

            if (x > 511 || y > 511) return null;

            var f1 = terrain.Get2DFromTile(x, y);
            var f2 = terrain.Get2DFromTile(x+1, y+1);
            if (f1.X == float.MaxValue || f2.X == float.MaxValue) return Vector2.Zero;
            var to = (terrain.Get2DFromTile(x, y) + terrain.Get2DFromTile(x+1, y+1)) / 2;
            return to;
        } 

        public static void DrawArrow(UISpriteBatch batch, Terrain terrain, Vector2 from, int location, Color tint)
        {
            if (!terrain.Visible) return;
            Vector2? dest = GetEndpointFromLotId(terrain, from, location);
            if (!dest.HasValue) return;

            Vector2 to = dest.Value;
            var vector = to - from;
            var norm = vector;
            norm.Normalize();

            var fill = TextureGenerator.GetPxWhite(batch.GraphicsDevice);

            var normr = new Vector2(-norm.Y, norm.X);

            var shadOffset = new Vector2(0, 4f);
            var col = Color.Black * 0.5f;
            from += shadOffset;
            to += shadOffset;

            for (var j = 0; j < 2; j++)
            {
                if (j == 1)
                {
                    col = tint; //arrows can now be any color 
                    from -= shadOffset;
                    to -= shadOffset;
                }
                DrawLine(fill, from, to, batch, 5, col);                

                for (int i = 0; i < 5; i++)
                {
                    DrawLine(fill, to - (norm * (15 - i)) - normr * (8 - i), to + norm * (3 - i), batch, i + 1, col);
                    DrawLine(fill, to - (norm * (15 - i)) + normr * (8 - i), to + norm * (3 - i), batch, i + 1, col);
                    DrawLine(fill, to - (norm * (15 - i)) + normr * (8 - i), to - (norm * (15 - i)) - normr * (8 - i), batch, i + 1, col);
                }
            }
        }
    }
}
