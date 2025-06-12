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
    /// Defines the faces in a cube map for the <see cref="TextureCube"/> class.
    /// </summary>
	public enum CubeMapFace
    {
        /// <summary>
        /// Positive X face in the cube map.
        /// </summary>
		PositiveX,
        /// <summary>
        /// Negative X face in the cube map.
        /// </summary>
        NegativeX, 
        /// <summary>
        /// Positive Y face in the cube map.
        /// </summary>
        PositiveY,
        /// <summary>
        /// Negative Y face in the cube map.
        /// </summary>
        NegativeY,
        /// <summary>
        /// Positive Z face in the cube map.
        /// </summary>
        PositiveZ,
        /// <summary>
        /// Negative Z face in the cube map.
        /// </summary>
		NegativeZ
	}
}

