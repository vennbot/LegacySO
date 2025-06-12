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

namespace Microsoft.Xna.Framework.Input
{
    /// <summary> 
    /// Allows interaction with joysticks. Unlike <see cref="GamePad"/> the number of Buttons/Axes/DPads is not limited.
    /// </summary>
    public static partial class Joystick
    {
        /// <summary>
        /// Gets a value indicating whether the current platform supports reading raw joystick data.
        /// </summary>
        /// <value><c>true</c> if the current platform supports reading raw joystick data; otherwise, <c>false</c>.</value>
        public static bool IsSupported
        {
            get { return PlatformIsSupported; }
        }

        /// <summary>
        /// Gets the capabilites of the joystick.
        /// </summary>
        /// <param name="index">Index of the joystick you want to access.</param>
        /// <returns>The capabilites of the joystick.</returns>
        public static JoystickCapabilities GetCapabilities(int index)
        {
            return PlatformGetCapabilities(index);
        }

        /// <summary>
        /// Gets the current state of the joystick.
        /// </summary>
        /// <param name="index">Index of the joystick you want to access.</param>
        /// <returns>The state of the joystick.</returns>
        public static JoystickState GetState(int index)
        {
            return PlatformGetState(index);
        }
    }
}
