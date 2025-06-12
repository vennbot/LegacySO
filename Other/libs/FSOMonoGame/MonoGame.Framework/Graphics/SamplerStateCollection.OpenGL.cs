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
// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
//
// Author: Kenneth James Pouncey

using MonoGame.OpenGL;

namespace Microsoft.Xna.Framework.Graphics
{
    public sealed partial class SamplerStateCollection
    {
        private void PlatformSetSamplerState(int index)
        {
        }

        private void PlatformClear()
        {
        }

        private void PlatformDirty()
        {
        }

        internal void PlatformSetSamplers(GraphicsDevice device)
        {
            for (var i = 0; i < _actualSamplers.Length; i++)
            {
                var sampler = _actualSamplers[i];
                var texture = device.Textures[i];

                if (sampler != null && texture != null && sampler != texture.glLastSamplerState)
                {
                    // TODO: Avoid doing this redundantly (see TextureCollection.SetTextures())
                    // However, I suspect that rendering from the same texture with different sampling modes
                    // is a relatively rare occurrence...
                    GL.ActiveTexture(TextureUnit.Texture0 + i);
                    GraphicsExtensions.CheckGLError();

                    // NOTE: We don't have to bind the texture here because it is already bound in
                    // TextureCollection.SetTextures(). This, of course, assumes that SetTextures() is called
                    // before this method is called. If that ever changes this code will misbehave.
                    // GL.BindTexture(texture.glTarget, texture.glTexture);
                    // GraphicsExtensions.CheckGLError();

                    sampler.Activate(device, texture.glTarget, texture.LevelCount > 1);
                    texture.glLastSamplerState = sampler;
                }
            }
        }
	}
}
