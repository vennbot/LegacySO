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
using FSO.Common.Rendering.Framework;
using System.Linq;
using FSO.Common.Rendering.Framework.Model;
using Microsoft.Xna.Framework.Graphics;
using FSO.Files.RC;

namespace FSO.LotView.Debug
{
    public class Debug3DDGRPComponent : _3DComponent
    {
        public DGRP3DMesh Mesh;
        public BasicEffect Effect;
        public bool Wireframe;

        public override void DeviceReset(GraphicsDevice Device)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Effect = new BasicEffect(Device);
        }

        public override void Draw(GraphicsDevice device)
        {
            if (Mesh == null) return;
            Effect.World = World;
            Effect.View = View;
            Effect.Projection = Projection;
            Effect.TextureEnabled = true;
            Effect.LightingEnabled = false;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.FillMode = Wireframe?FillMode.WireFrame:FillMode.Solid;
            rasterizerState.CullMode = CullMode.None;
            device.RasterizerState = rasterizerState;

            var gs = Mesh.Geoms.FirstOrDefault();
            if (gs == null) return;
            foreach (var geom in gs.Values)
            {
                if (geom.PrimCount == 0) continue;
                Effect.Texture = geom.Pixel;
                foreach (var pass in Effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    device.SamplerStates[0] = SamplerState.LinearClamp;
                    if (!geom.Rendered) continue;
                    device.Indices = geom.Indices;
                    device.SetVertexBuffer(geom.Verts);
                    device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, geom.PrimCount);
                }
            }
        }

        public override void Update(UpdateState state)
        {
        }

        public void Dispose()
        {
            //since these are loaded from the cache we can't dispose them
            //they could be being used ingame.
            /*
            if (Mesh != null)
            {
                foreach (var geom in Mesh.Geoms)
                {
                    foreach (var e in geom.Values) e.Dispose();
                }
            }
            */
        }
    }
}
