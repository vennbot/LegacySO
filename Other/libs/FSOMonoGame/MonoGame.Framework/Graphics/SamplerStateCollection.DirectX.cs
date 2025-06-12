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

namespace Microsoft.Xna.Framework.Graphics
{
    public sealed partial class SamplerStateCollection
    {
        private int _d3dDirty;

        private void PlatformSetSamplerState(int index)
        {
            _d3dDirty |= 1 << index;
        }

        private void PlatformClear()
        {
            _d3dDirty = int.MaxValue;
        }

        private void PlatformDirty()
        {
            _d3dDirty = int.MaxValue;
        }

        internal void PlatformSetSamplers(GraphicsDevice device)
        {
            if (_applyToVertexStage && !device.GraphicsCapabilities.SupportsVertexTextures)
                return;

            // Skip out if nothing has changed.
            if (_d3dDirty == 0)
                return;

            // NOTE: We make the assumption here that the caller has
            // locked the d3dContext for us to use.
            SharpDX.Direct3D11.CommonShaderStage shaderStage;
            if (_applyToVertexStage)
	            shaderStage = device._d3dContext.VertexShader;
            else
	            shaderStage = device._d3dContext.PixelShader;

            for (var i = 0; i < _actualSamplers.Length; i++)
            {
                var mask = 1 << i;
                if ((_d3dDirty & mask) == 0)
                    continue;

                var sampler = _actualSamplers[i];
                SharpDX.Direct3D11.SamplerState state = null;
                if (sampler != null)
                    state = sampler.GetState(device);

                shaderStage.SetSampler(i, state);

                _d3dDirty &= ~mask;
                if (_d3dDirty == 0)
                    break;
            }

            _d3dDirty = 0;
        }
    }
}
