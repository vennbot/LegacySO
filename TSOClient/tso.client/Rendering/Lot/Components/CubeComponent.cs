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
using TSOClient.Code.Rendering.Lot.Model;
using Microsoft.Xna.Framework;

namespace TSOClient.Code.Rendering.Lot.Components
{
    /// <summary>
    /// A 3D cube for debugging
    /// </summary>
    public class CubeComponent : House3DComponent
    {
        private BasicEffect Effect;

        private VertexPositionColor[] Geom;
        private List<VertexPositionColor> GeomList;

        public CubeComponent(Color color, Vector3 size)
        {
            Effect = new BasicEffect(GameFacade.GraphicsDevice, null);

            /** Bottom Face **/
            var btmTL = new Vector3(0.0f, 0.0f, 0.0f);
            var btmTR = new Vector3(size.X, 0.0f, 0.0f);
            var btmBR = new Vector3(size.X, 0.0f, size.Z);
            var btmBL = new Vector3(0.0f, 0.0f, size.Z);

            /** Top face **/
            var topTL = new Vector3(0.0f, size.Y, 0.0f);
            var topTR = new Vector3(size.X, size.Y, 0.0f);
            var topBR = new Vector3(size.X, size.Y, size.Z);
            var topBL = new Vector3(0.0f, size.Y, size.Z);


            GeomList = new List<VertexPositionColor>();
            AddQuad(color, topTL, topTR, topBR, topBL);
            AddQuad(Color.Yellow, btmTL, btmTR, btmBR, btmBL);
            AddQuad(Color.Green, topTL, topTR, btmTR, btmTL);
            AddQuad(Color.Blue, topBL, topTL, btmTL, btmBL);
            AddQuad(Color.Orange, topBR, topTR, btmTR, btmBR);
            AddQuad(Color.White, topBL, topBR, btmBR, btmBL);

            Geom = GeomList.ToArray();
        }


        private void AddQuad(Color color, Vector3 tl, Vector3 tr, Vector3 br, Vector3 bl)
        {
            GeomList.Add(new VertexPositionColor(tl, color));
            GeomList.Add(new VertexPositionColor(tr, color));
            GeomList.Add(new VertexPositionColor(br, color));

            GeomList.Add(new VertexPositionColor(br, color));
            GeomList.Add(new VertexPositionColor(bl, color));
            GeomList.Add(new VertexPositionColor(tl, color));
        }

        public override void Draw(GraphicsDevice device, HouseRenderState state)
        {
            device.VertexDeclaration = new VertexDeclaration(device, VertexPositionColor.VertexElements);


            Effect.World = state.World * Matrix.CreateTranslation(Position);
            Effect.View = state.Camera.View;
            Effect.Projection = state.Camera.Projection;
            Effect.VertexColorEnabled = true;
            //Effect.EnableDefaultLighting();

            Effect.Begin();
            foreach (var pass in Effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, Geom, 0, Geom.Length / 3);
                pass.End();
            }
            Effect.End();
        }
    }
}
