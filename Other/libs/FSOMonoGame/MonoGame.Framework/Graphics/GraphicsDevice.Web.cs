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

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace Microsoft.Xna.Framework.Graphics
{
    using MonoGame.Web;

    public partial class GraphicsDevice
    {
        private void PlatformSetup()
        {
            
        }

        private void PlatformInitialize()
        {
        }

        internal void OnPresentationChanged()
        {
        }

        public void PlatformClear(ClearOptions options, Vector4 color, float depth, int stencil)
        {
            WebGL.gl.enable(WebGL.gl.DEPTH_TEST);
            WebGL.gl.depthFunc(WebGL.gl.LEQUAL);
            WebGL.gl.clearColor(color.X, color.Y, color.Z, color.W);
            WebGL.gl.clear(WebGL.gl.COLOR_BUFFER_BIT | WebGL.gl.DEPTH_BUFFER_BIT);
        }

        private void PlatformDispose()
        {
        }

        public void PlatformPresent()
        {
        }

        private void PlatformSetViewport(ref Viewport value)
        {
        }

        private void PlatformApplyDefaultRenderTarget()
        {
        }

        internal void PlatformResolveRenderTargets()
        {
            // Resolving MSAA render targets should be done here.
        }

        private IRenderTarget PlatformApplyRenderTargets()
        {
            return null;
        }
		
        internal void PlatformBeginApplyState()
        {
        }

        private void PlatformApplyBlend()
        {
        }

        internal void PlatformApplyState(bool applyShaders)
        {
        }

        private void PlatformDrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int startIndex, int primitiveCount)
        {
        }

        private void PlatformDrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, VertexDeclaration vertexDeclaration, int vertexCount) where T : struct
        {
        }

        private void PlatformDrawPrimitives(PrimitiveType primitiveType, int vertexStart, int vertexCount)
        {
        }

        private void PlatformDrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, short[] indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct
        {
        }

        private void PlatformDrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, int[] indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct
        {
        }

        private void PlatformDrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int startIndex, int primitiveCount, int instanceCount)
        {
        }

        private void PlatformGetBackBufferData<T>(Rectangle? rect, T[] data, int startIndex, int count) where T : struct
        {
            throw new NotImplementedException();
        }

        private static Rectangle PlatformGetTitleSafeArea(int x, int y, int width, int height)
        {
            return new Rectangle(x, y, width, height);
        }
        
        internal void PlatformSetMultiSamplingToMaximum(PresentationParameters presentationParameters, out int quality)
        {
            presentationParameters.MultiSampleCount = 0;
            quality = 0;
        }
    }
}
