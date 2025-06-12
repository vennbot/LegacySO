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
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Input
{
    internal class ButtonType
    {
        public bool button { get; set; }
        public int index { get; set; }
    }

    internal class GamepadTranslator
    {
        Dictionary<string, ButtonType> Data;

        public GamepadTranslator(string config)
        {
            Data = new Dictionary<string, ButtonType>();
            var split = config.Split(',');

            for (int i = 1; i < split.Length; i++)
            {
                var split2 = split[i].Split(':');
                var btype = new ButtonType()
                    {
                        button = (split2[1][0] == 'b'),
                        index = Convert.ToInt32(split2[1].Substring(1))
                    };
                
                Data.Add(split2[0], btype);
            }
        }

        public ButtonType Read(string button)
        {
            return Data.ContainsKey(button) ? Data[button] : new ButtonType() { index = -1 };
        }

        public bool ButtonPressed(string button, JoystickState state)
        {
            var type = Read(button);

            if (type.index != -1)
            {
                if (type.button)
                    return state.Buttons[type.index] == ButtonState.Pressed;
                else
                    return state.Axes[type.index] == 1 || state.Axes[type.index] == -1;
            }

            return false;
        }

        public float AxisPressed(string axis, JoystickState state)
        {
            var type = Read(axis);
            return type.index != -1 ? state.Axes[type.index] : 0f;
        }

        public bool DpadPressed(string dpad, JoystickState state)
        {
            var type = Read(dpad);

            if (type.index != -1)
            {
                if (type.button)
                    return state.Buttons[type.index] == ButtonState.Pressed;
                else if(dpad == "dpright" || dpad == "dpdown")
                    return state.Axes[type.index] == 1;
                else
                    return state.Axes[type.index] == -1;
            }

            return false;
        }

        public float TriggerPressed(string trigger, JoystickState state)
        {
            var type = Read(trigger);

            if (type.index != -1)
            {
                if (type.button)
                    return (state.Buttons[type.index] == ButtonState.Pressed) ? 1f : 0f;
                else
                    return Math.Max(state.Axes[type.index], 0f);
            }

            return 0f;
        }
    }
}

