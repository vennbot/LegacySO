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
    /// Defines how <see cref="GraphicsDevice.Present"/> updates the game window.
    /// </summary>
    public enum PresentInterval
    {
        /// <summary>
        /// Equivalent to <see cref="PresentInterval.One"/>.
        /// </summary>
        Default,
        /// <summary>
        /// The driver waits for the vertical retrace period, before updating window client area. Present operations are not affected more frequently than the screen refresh rate.
        /// </summary>
        One,
        /// <summary>
        /// The driver waits for the vertical retrace period, before updating window client area. Present operations are not affected more frequently than every second screen refresh. 
        /// </summary>
        Two,
        /// <summary>
        /// The driver updates the window client area immediately. Present operations might be affected immediately. There is no limit for framerate.
        /// </summary>
        Immediate,
    }
}
