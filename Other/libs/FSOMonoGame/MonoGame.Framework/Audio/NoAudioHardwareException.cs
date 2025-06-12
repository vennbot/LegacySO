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
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework.Audio
{

    /// <summary>
    /// The exception thrown when no audio hardware is present, or driver issues are detected.
    /// </summary>
    [DataContract]
#if WINDOWS_UAP
    public sealed class NoAudioHardwareException : Exception
#else
    public sealed class NoAudioHardwareException : ExternalException
#endif
    {
        /// <param name="msg">A message describing the error.</param>
        public NoAudioHardwareException(string msg)
            : base(msg)
        {
        }

        /// <param name="msg">A message describing the error.</param>
        /// <param name="innerException">The exception that is the underlying cause of the current exception. If not null, the current exception is raised in a try/catch block that handled the innerException.</param>
        public NoAudioHardwareException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }
    }
}

