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
    /// Defines how vertex data is ordered.
    /// </summary>
	public enum PrimitiveType
	{
        /// <summary>
        /// Renders the specified vertices as a sequence of isolated triangles. Each group of three vertices defines a separate triangle. Back-face culling is affected by the current winding-order render state.
        /// </summary>
		TriangleList,

        /// <summary>
        /// Renders the vertices as a triangle strip. The back-face culling flag is flipped automatically on even-numbered triangles.
        /// </summary>
		TriangleStrip,

        /// <summary>
        /// Renders the vertices as a list of isolated straight line segments; the count may be any positive integer.
        /// </summary>
		LineList,

        /// <summary>
        /// Renders the vertices as a single polyline; the count may be any positive integer.
        /// </summary>
		LineStrip,
	}
}
