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
    internal partial class GraphicsCapabilities
    {
        private void PlatformInitialize(GraphicsDevice device)
        {
            SupportsNonPowerOfTwo = device.GraphicsProfile == GraphicsProfile.HiDef;
            SupportsTextureFilterAnisotropic = true;

            SupportsDepth24 = true;
            SupportsPackedDepthStencil = true;
            SupportsDepthNonLinear = false;
            SupportsTextureMaxLevel = true;

            // Texture compression
            SupportsDxt1 = true;
            SupportsS3tc = true;

            SupportsSRgb = true;

            SupportsTextureArrays = device.GraphicsProfile == GraphicsProfile.HiDef;
            SupportsDepthClamp = device.GraphicsProfile == GraphicsProfile.HiDef;
            SupportsVertexTextures = device.GraphicsProfile == GraphicsProfile.HiDef;
            SupportsFloatTextures = true;
            SupportsHalfFloatTextures = true;
            SupportsNormalized = true;

            SupportsInstancing = true;

            MaxTextureAnisotropy = (device.GraphicsProfile == GraphicsProfile.Reach) ? 2 : 16;

            _maxMultiSampleCount = GetMaxMultiSampleCount(device);
        }

        private int GetMaxMultiSampleCount(GraphicsDevice device)
        {
            var format = SharpDXHelper.ToFormat(device.PresentationParameters.BackBufferFormat);
            // Find the maximum supported level starting with the game's requested multisampling level
            // and halving each time until reaching 0 (meaning no multisample support).
            var qualityLevels = 0;
            var maxLevel = MultiSampleCountLimit;
            while (maxLevel > 0)
            {
                qualityLevels = device._d3dDevice.CheckMultisampleQualityLevels(format, maxLevel);
                if (qualityLevels > 0)
                    break;
                maxLevel /= 2;
            }
            return maxLevel;
        }
    }
}
