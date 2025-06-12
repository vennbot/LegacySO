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

namespace Microsoft.Xna.Framework.Content.Pipeline.Processors
{
    /// <summary>
    /// Specifies the target output (of type SurfaceFormat) of the texture processor. Used by TextureProcessor.TextureFormat.
    /// </summary>
    public enum TextureProcessorOutputFormat
    {
        /// <summary>
        /// The SurfaceFormat value, of the input TextureContent object, is converted to Color by the processor. Typically used for 2D graphics and overlays.
        /// </summary>
        Color,

        /// <summary>
        /// The SurfaceFormat value, of the input TextureContent object, is converted to an appropriate DXT compression by the processor. If the input texture
        /// contains fractional alpha values, it is converted to DXT5 format (8 bits per texel); otherwise it is converted to DXT1 (4 bits per texel). This
        /// conversion reduces the resource's size on the graphics card. Typically used for 3D textures such as 3D model textures.
        /// </summary>
        DxtCompressed,

        /// <summary>
        /// The SurfaceFormat value, of the input TextureContent object, is not changed by the processor. Typically used for textures processed by an external tool.
        /// </summary>
        NoChange,

        /// <summary>
        /// The SurfaceFormat value, of the input TextureContent object, is converted to an appropriate compressed format for the target platform.
        /// This can include PVRTC for iOS, DXT for desktop, Windows 8 and Windows Phone 8, and ETC1 or BGRA4444 for Android.
        /// </summary>
        Compressed,

        /// <summary>
        /// The pixel depth of the input texture is reduced to BGR565 for opaque textures, otherwise it uses BGRA4444.
        /// </summary>
        Color16Bit,

        /// <summary>
        /// The input texture is compressed using ETC1 texture compression.  Used on Android platforms.
        /// </summary>
        Etc1Compressed,

        /// <summary>
        /// The input texture is compressed using PVR texture compression. Used on iOS and some Android platforms.
        /// </summary>
        PvrCompressed,

        /// <summary>
        /// The input texture is compressed using ATI texture compression.  Used on some Android platforms.
        /// </summary>
        AtcCompressed,
    }
}
