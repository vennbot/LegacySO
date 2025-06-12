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
    /// Defines vertex element formats.
    /// </summary>
    public enum VertexElementFormat
    {
        /// <summary>
        /// Single 32-bit floating point number.
        /// </summary>
        Single,
        /// <summary>
        /// Two component 32-bit floating point number.
        /// </summary>
        Vector2,
        /// <summary>
        /// Three component 32-bit floating point number.
        /// </summary>
        Vector3, 
        /// <summary>
        /// Four component 32-bit floating point number.
        /// </summary>
        Vector4, 
        /// <summary>
        /// Four component, packed unsigned byte, mapped to 0 to 1 range. 
        /// </summary>
        Color,   
        /// <summary>
        /// Four component unsigned byte.
        /// </summary>
        Byte4,        
        /// <summary>
        /// Two component signed 16-bit integer.
        /// </summary>
        Short2,
        /// <summary>
        /// Four component signed 16-bit integer.
        /// </summary>
        Short4,
        /// <summary>
        /// Normalized, two component signed 16-bit integer.
        /// </summary>
        NormalizedShort2,
        /// <summary>
        /// Normalized, four component signed 16-bit integer.
        /// </summary>
        NormalizedShort4,
        /// <summary>
        /// Two component 16-bit floating point number.
        /// </summary>
        HalfVector2,  
        /// <summary>
        /// Four component 16-bit floating point number.
        /// </summary>
        HalfVector4
    }
}
