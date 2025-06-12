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

using System;

namespace Microsoft.Xna.Framework.Input
{
    static partial class Joystick
    {
        private const bool PlatformIsSupported = false;

        private static JoystickCapabilities PlatformGetCapabilities(int index)
        {
            return new JoystickCapabilities()
            {
                IsConnected = false,
                IsGamepad = false,
                AxisCount = 0,
                ButtonCount = 0,
                HatCount = 0
            };
        }

        private static JoystickState PlatformGetState(int index)
        {
            return new JoystickState()
            {
                IsConnected = false,
                Axes = new int[0],
                Buttons = new ButtonState[0],
                Hats = new JoystickHat[0]
            };
        }
    }
}

