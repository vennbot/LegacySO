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
    /// Defines a type of gamepad.
    /// </summary>
    public enum GamePadType
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// GamePad is the XBOX controller.
        /// </summary>
        GamePad,

        /// <summary>
        /// GamePad is a wheel.
        /// </summary>
        Wheel,

        /// <summary>
        /// GamePad is an arcade stick.
        /// </summary>
        ArcadeStick,

        /// <summary>
        /// GamePad is a flight stick.
        /// </summary>
        FlightStick,

        /// <summary>
        /// GamePad is a dance pad.
        /// </summary>
        DancePad,

        /// <summary>
        /// GamePad is a guitar.
        /// </summary>
        Guitar,

        /// <summary>
        /// GamePad is an alternate guitar.
        /// </summary>
        AlternateGuitar,

        /// <summary>
        /// GamePad is a drum kit.
        /// </summary>
        DrumKit,

        /// <summary>
        /// GamePad is a big button pad.
        /// </summary>
        BigButtonPad = 768
    }
}
