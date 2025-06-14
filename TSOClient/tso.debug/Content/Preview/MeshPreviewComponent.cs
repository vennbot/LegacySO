
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
using FSO.Common.Rendering.Framework;
using FSO.Vitaboy;
using Microsoft.Xna.Framework.Graphics;

namespace FSO.Debug.content.preview
{
    public class MeshPreviewComponent : _3DComponent
    {
        public Mesh Mesh;
        public Texture2D Texture;

        public override void Update(FSO.Common.Rendering.Framework.Model.UpdateState state)
        {
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
        {
            if (Mesh == null) { return; }

            var effect = new BasicEffect(device);
            effect.World = World;
            effect.View = View;
            effect.Projection = Projection;
            if (Texture != null)
            {
                effect.TextureEnabled = true;
                effect.Texture = Texture;
            }

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Mesh.Draw(device);
            }
        }

        public override void DeviceReset(GraphicsDevice Device)
        {
            throw new NotImplementedException();
        }
    }
}
