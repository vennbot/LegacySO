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
	/// Defines types for effect parameters and shader constants.
	/// </summary>
	public enum EffectParameterType
	{
        /// <summary>
        /// Pointer to void type.
        /// </summary>
		Void,
        /// <summary>
        /// Boolean type. Any non-zero will be <c>true</c>; <c>false</c> otherwise.
        /// </summary>
		Bool,
        /// <summary>
        /// 32-bit integer type.
        /// </summary>
		Int32,
        /// <summary>
        /// Float type.
        /// </summary>
		Single,
        /// <summary>
        /// String type.
        /// </summary>
		String,
        /// <summary>
        /// Any texture type.
        /// </summary>
		Texture,
        /// <summary>
        /// 1D-texture type.
        /// </summary>
        Texture1D,  
        /// <summary>
        /// 2D-texture type.
        /// </summary>
        Texture2D,
        /// <summary>
        /// 3D-texture type.
        /// </summary>
        Texture3D,
        /// <summary>
        /// Cubic texture type.
        /// </summary>
		TextureCube
	}
}
