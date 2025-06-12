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
	/// Defines formats for depth-stencil buffer.
	/// </summary>
    public enum DepthFormat
    {
        /// <summary>
        /// Depth-stencil buffer will not be created.
        /// </summary>
		None,
        /// <summary>
        /// 16-bit depth buffer.
        /// </summary>
		Depth16,
        /// <summary>
        /// 24-bit depth buffer. Equivalent of <see cref="DepthFormat.Depth24Stencil8"/> for DirectX platforms.
        /// </summary>
		Depth24,
        /// <summary>
        /// 32-bit depth-stencil buffer. Where 24-bit depth and 8-bit for stencil used.
        /// </summary>
		Depth24Stencil8
    }
}
