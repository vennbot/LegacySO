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
    /// <summary>
    /// Defines filtering types for texture sampler.
    /// </summary>
	public enum TextureFilter
	{
        /// <summary>
        /// Use linear filtering.
        /// </summary>
		Linear,
        /// <summary>
        /// Use point filtering.
        /// </summary>
		Point,
        /// <summary>
        /// Use anisotropic filtering.
        /// </summary>
		Anisotropic,	
        /// <summary>
        /// Use linear filtering to shrink or expand, and point filtering between mipmap levels (mip).
        /// </summary>
		LinearMipPoint,
        /// <summary>
        /// Use point filtering to shrink (minify) or expand (magnify), and linear filtering between mipmap levels.
        /// </summary>
		PointMipLinear,
        /// <summary>
        /// Use linear filtering to shrink, point filtering to expand, and linear filtering between mipmap levels.
        /// </summary>
		MinLinearMagPointMipLinear,
        /// <summary>
        /// Use linear filtering to shrink, point filtering to expand, and point filtering between mipmap levels.
        /// </summary>
		MinLinearMagPointMipPoint,
        /// <summary>
        /// Use point filtering to shrink, linear filtering to expand, and linear filtering between mipmap levels.
        /// </summary>
		MinPointMagLinearMipLinear,
        /// <summary>
        /// Use point filtering to shrink, linear filtering to expand, and point filtering between mipmap levels.
        /// </summary>
		MinPointMagLinearMipPoint
	}
}
