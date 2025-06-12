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
using JSIL;

namespace Microsoft.Xna.Framework.Input
{
    static partial class Joystick
    {
        private const bool PlatformIsSupported = true;

        internal static bool TrackEvents = false;

        private static JoystickCapabilities PlatformGetCapabilities(int index)
        {
            bool connected = false;
            string id = "";
            int axiscount = 0;
            int buttoncount = 0;

            var navigator = Builtins.Global["navigator"];
            var gamepads = navigator.getGamepads ? navigator.getGamepads() : navigator.webkitGetGamepads();

            if (gamepads.length > index)
            {
                if (gamepads[index])
                {
                    connected = true;
                    id = gamepads[index].id;
                    axiscount = gamepads[index].axes.length;
                    buttoncount = gamepads[index].buttons.length;
                }
            }

            return new JoystickCapabilities()
            {
                IsConnected = connected,
                Identifier = id,
                AxisCount = axiscount,
                ButtonCount = buttoncount,
                HatCount = 0
            };
        }

        private static JoystickState PlatformGetState(int index)
        {
            var connected = false;
            var axes = new int[0];
            var buttons = new ButtonState[0];

            var navigator = Builtins.Global["navigator"];
            var gamepads = navigator.getGamepads ? navigator.getGamepads() : navigator.webkitGetGamepads();

            if (gamepads.length > index)
            {
                if (gamepads[index])
                {
                    connected = true;

                    var axescount = gamepads[index].axes.length;
                    axes = new int[gamepads[index].axes.length];

                    for (int i = 0; i < axescount; i++)
                        axes[i] = gamepads[index].axes[i];

                    var buttoncount = gamepads[index].buttons.length;
                    buttons = new ButtonState[buttoncount];

                    for (int i = 0; i < buttoncount; i++)
                    {
                        if (gamepads[index].buttons[i].pressed)
                            buttons[i] = ButtonState.Pressed;
                        else
                            buttons[i] = ButtonState.Released;
                    }
                }
            }

            return new JoystickState()
            {
                IsConnected = connected,
                Axes = axes,
                Buttons = buttons,
                Hats = new JoystickHat[0]
            };
        }
    }
}
