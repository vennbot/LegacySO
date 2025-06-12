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
    /// <summary> 
    /// Supports querying the game controllers and setting the vibration motors.
    /// </summary>
    public static partial class GamePad
    {
        private static int PlatformGetMaxNumberOfGamePads()
        {
            return 0;
        }

        private static GamePadCapabilities PlatformGetCapabilities(int index)
        {
            return new GamePadCapabilities { IsConnected = false };
        }
               
        private static GamePadState PlatformGetState(int index, GamePadDeadZone deadZoneMode)
        {
            return new GamePadState() { IsConnected = false };
        }

        private static bool PlatformSetVibration(int index, float leftMotor, float rightMotor)
        {
            return false;
        }
    }
}

