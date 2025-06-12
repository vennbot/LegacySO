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


namespace Microsoft.Xna.Framework.Graphics
{
    internal partial class ConstantBuffer : GraphicsResource
    {
        private SharpDX.Direct3D11.Buffer _cbuffer;

        private void PlatformInitialize()
        {
            // Allocate the hardware constant buffer.
            var desc = new SharpDX.Direct3D11.BufferDescription();
            desc.SizeInBytes = _buffer.Length;
            desc.Usage = SharpDX.Direct3D11.ResourceUsage.Default;
            desc.BindFlags = SharpDX.Direct3D11.BindFlags.ConstantBuffer;
            desc.CpuAccessFlags = SharpDX.Direct3D11.CpuAccessFlags.None;
            lock (GraphicsDevice._d3dContext)
                _cbuffer = new SharpDX.Direct3D11.Buffer(GraphicsDevice._d3dDevice, desc);
        }

        private void PlatformClear()
        {
            SharpDX.Utilities.Dispose(ref _cbuffer);
            _dirty = true;
        }

        internal void PlatformApply(GraphicsDevice device, ShaderStage stage, int slot)
        {
            if (_cbuffer == null)
                PlatformInitialize();

            // NOTE: We make the assumption here that the caller has
            // locked the d3dContext for us to use.
            var d3dContext = GraphicsDevice._d3dContext;

            // Update the hardware buffer.
            if (_dirty)
            {
                d3dContext.UpdateSubresource(_buffer, _cbuffer);
                _dirty = false;
            }
            
            // Set the buffer to the right stage.
            if (stage == ShaderStage.Vertex)
                d3dContext.VertexShader.SetConstantBuffer(slot, _cbuffer);
            else
                d3dContext.PixelShader.SetConstantBuffer(slot, _cbuffer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                SharpDX.Utilities.Dispose(ref _cbuffer);
            base.Dispose(disposing);
        }
    }
}
