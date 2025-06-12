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
    /// Defines modes for addressing texels using texture coordinates that are outside of the range of 0.0 to 1.0.
    /// </summary>
	public enum TextureAddressMode
	{
        /// <summary>
        /// Texels outside range will form the tile at every integer junction.
        /// </summary>
		Wrap,
        /// <summary>
        /// Texels outside range will be set to color of 0.0 or 1.0 texel.
        /// </summary>
		Clamp,
        /// <summary>
        /// Same as <see cref="TextureAddressMode.Wrap"/> but tiles will also flipped at every integer junction.
        /// </summary>
        Mirror,
        /// <summary>
        /// Texels outside range will be set to the border color.
        /// </summary>
        Border
	}
}
