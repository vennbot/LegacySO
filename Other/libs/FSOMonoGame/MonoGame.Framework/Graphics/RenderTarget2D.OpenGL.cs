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

using MonoGame.OpenGL;
using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
    public partial class RenderTarget2D
    {
        int IRenderTarget.GLTexture
        {
            get { return glTexture; }
        }

        TextureTarget IRenderTarget.GLTarget
        {
            get { return glTarget; }
        }

        int IRenderTarget.GLColorBuffer { get; set; }
        int IRenderTarget.GLDepthBuffer { get; set; }
        int IRenderTarget.GLStencilBuffer { get; set; }

        TextureTarget IRenderTarget.GetFramebufferTarget(RenderTargetBinding renderTargetBinding)
        {
            return glTarget;
        }

        private void PlatformConstruct(GraphicsDevice graphicsDevice, int width, int height, bool mipMap,
            DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage, bool shared)
        {
            Threading.BlockOnUIThread(() =>
            {
                graphicsDevice.PlatformCreateRenderTarget(this, width, height, mipMap, this.Format, preferredDepthFormat, preferredMultiSampleCount, usage);
            });
            
            
        }

        private void PlatformGraphicsDeviceResetting()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                Threading.BlockOnUIThread(() =>
                {
                    this.GraphicsDevice.PlatformDeleteRenderTarget(this);
                });
            }

            base.Dispose(disposing);
        }

        public void InheritDepthStencil(RenderTarget2D from)
        {
            if (from == null)
            {
                //must use other method
            }
            else
            {
                ((IRenderTarget)this).GLDepthBuffer = ((IRenderTarget)from).GLDepthBuffer;
                ((IRenderTarget)this).GLStencilBuffer = ((IRenderTarget)from).GLStencilBuffer;
            }
        }
    }
}
