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
    /// Defines the continuity of keys on a <see cref="Curve"/>.
    /// </summary>
    public enum CurveContinuity
    {
        /// <summary>
        /// Interpolation can be used between this key and the next.
        /// </summary>
        Smooth,
        /// <summary>
        /// Interpolation cannot be used. A position between the two points returns this point.
        /// </summary>
        Step
    }
}
