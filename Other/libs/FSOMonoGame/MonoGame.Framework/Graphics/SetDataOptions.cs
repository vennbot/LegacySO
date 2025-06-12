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
    /// Defines how vertex or index buffer data will be flushed during a SetData operation.
    /// </summary>
    public enum SetDataOptions
    { 
        /// <summary>
        /// The SetData can overwrite the portions of existing data.
        /// </summary>
        None,
        /// <summary>
        /// The SetData will discard the entire buffer. A pointer to a new memory area is returned and rendering from the previous area do not stall.
        /// </summary>
        Discard,
        /// <summary>
        /// The SetData operation will not overwrite existing data. This allows the driver to return immediately from a SetData operation and continue rendering.
        /// </summary>
        NoOverwrite
    }
}
