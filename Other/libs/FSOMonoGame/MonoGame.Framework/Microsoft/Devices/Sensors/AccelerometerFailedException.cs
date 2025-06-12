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

namespace Microsoft.Devices.Sensors
{
    /// <summary>
    /// The exception that may be thrown during a call to Start() or Stop(). The Message field describes the reason for the exception and the ErrorId field contains the error code from the underlying native code implementation of the accelerometer framework.
    /// </summary>
    public class AccelerometerFailedException : SensorFailedException
    {
        /// <summary>
        /// Initializes a new instance of AccelerometerFailedException
        /// </summary>
        /// <param name="message">The descriptive reason for the exception</param>
        /// <param name="errorId">The native error code that caused the exception</param>
        internal AccelerometerFailedException(string message, int errorId)
            : base(message)
        {
            ErrorId = errorId;
        }
    }
}
