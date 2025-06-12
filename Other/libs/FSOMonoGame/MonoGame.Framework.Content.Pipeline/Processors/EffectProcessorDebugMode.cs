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

namespace Microsoft.Xna.Framework.Content.Pipeline.Processors
{
    /// <summary>
    /// Specifies how debugging of effects is to be supported in PIX.
    /// </summary>
    public enum EffectProcessorDebugMode
    {
        /// <summary>
        /// Enables effect debugging when built with Debug profile.
        /// </summary>
        Auto = 0,

        /// <summary>
        /// Enables effect debugging for all profiles. Will produce unoptimized shaders.
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Disables debugging for all profiles, produce optimized shaders.
        /// </summary>
        Optimize = 2,
    }
}
