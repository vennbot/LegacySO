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
    /// Defines if the previous content in a render target is preserved when it set on the graphics device.
    /// </summary>
    public enum RenderTargetUsage
    {
        /// <summary>
        /// The render target content will not be preserved.
        /// </summary>
        DiscardContents,
        /// <summary>
        /// The render target content will be preserved even if it is slow or requires extra memory.
        /// </summary>
        PreserveContents,
        /// <summary>
        /// The render target content might be preserved if the platform can do so without a penalty in performance or memory usage.
        /// </summary>
        PlatformContents
    }
}

