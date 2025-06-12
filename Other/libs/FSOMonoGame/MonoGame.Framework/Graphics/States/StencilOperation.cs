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
    /// Defines stencil buffer operations.
    /// </summary>
    public enum StencilOperation
    {
        /// <summary>
        /// Does not update the stencil buffer entry.
        /// </summary>
        Keep,
        /// <summary>
        /// Sets the stencil buffer entry to 0.
        /// </summary>
        Zero,
        /// <summary>
        /// Replaces the stencil buffer entry with a reference value.
        /// </summary>
        Replace,
        /// <summary>
        /// Increments the stencil buffer entry, wrapping to 0 if the new value exceeds the maximum value.
        /// </summary>
        Increment,
        /// <summary>
        /// Decrements the stencil buffer entry, wrapping to the maximum value if the new value is less than 0.
        /// </summary>
        Decrement,
        /// <summary>
        /// Increments the stencil buffer entry, clamping to the maximum value.
        /// </summary>
        IncrementSaturation,
        /// <summary>
        /// Decrements the stencil buffer entry, clamping to 0.
        /// </summary>
        DecrementSaturation,
        /// <summary>
        /// Inverts the bits in the stencil buffer entry.
        /// </summary>
        Invert
    }
}
