
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

namespace FSO.LotView.Utils
{
    public class _2DStandaloneSprite: _2DSprite, IDisposable
    {
        public static int[] indices = new int[] { 0, 1, 3, 1, 2, 3 };

        public _2DSpriteVertex[] vertices;
        public VertexBuffer GPUVertices;

        public void Dispose()
        {
            GPUVertices?.Dispose();
        }

        public void PrepareVertices(GraphicsDevice gd)
        {
            var srcRectangle = SrcRect;
            var dstRectangle = AbsoluteDestRect;
            var texture = Pixel;
            // add the new vertices

            var left = FlipHorizontally ? srcRectangle.Right : srcRectangle.Left;
            var right = FlipHorizontally ? srcRectangle.Left : srcRectangle.Right;
            var top = FlipVertically ? srcRectangle.Bottom : srcRectangle.Top;
            var bot = FlipVertically ? srcRectangle.Top : srcRectangle.Bottom;

            vertices = new _2DSpriteVertex[] {
                new _2DSpriteVertex(
                    new Vector3(dstRectangle.Left, dstRectangle.Top, 0)
                    , GetUV(texture, left, top), AbsoluteWorldPosition, ObjectID, Room, Floor),
                new _2DSpriteVertex(
                    new Vector3(dstRectangle.Right, dstRectangle.Top, 0)
                    , GetUV(texture, right, top), AbsoluteWorldPosition, ObjectID, Room, Floor),
                new _2DSpriteVertex(
                    new Vector3(dstRectangle.Right, dstRectangle.Bottom, 0)
                    , GetUV(texture, right, bot), AbsoluteWorldPosition, ObjectID, Room, Floor),
                new _2DSpriteVertex(
                    new Vector3(dstRectangle.Left, dstRectangle.Bottom, 0)
                    , GetUV(texture, left, bot), AbsoluteWorldPosition, ObjectID, Room, Floor)
            };

            if (GPUVertices == null) GPUVertices = new VertexBuffer(gd, typeof(_2DSpriteVertex), 4, BufferUsage.None);
            GPUVertices.SetData(vertices);
        }

        private Vector2 GetUV(Texture2D Texture, float x, float y)
        {
            return new Vector2(x / (float)Texture.Width, y / (float)Texture.Height);
        }
    }
}
