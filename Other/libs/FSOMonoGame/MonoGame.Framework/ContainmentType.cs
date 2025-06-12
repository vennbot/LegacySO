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
// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Defines how the bounding volumes intersects or contain one another.
    /// </summary>
    public enum ContainmentType
    {
        /// <summary>
        /// Indicates that there is no overlap between two bounding volumes.
        /// </summary>
        Disjoint,
        /// <summary>
        /// Indicates that one bounding volume completely contains another volume.
        /// </summary>
        Contains,
        /// <summary>
        /// Indicates that bounding volumes partially overlap one another.
        /// </summary>
        Intersects
    }
}
