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
using System;

namespace Mina.Core.Session
{
    /// <summary>
    /// An exception that is thrown when the type of the message cannot be determined.
    /// </summary>
    [Serializable]
    public class UnknownMessageTypeException : Exception
    {
        /// <summary>
        /// </summary>
        public UnknownMessageTypeException() { }

        /// <summary>
        /// </summary>
        public UnknownMessageTypeException(String message) : base(message) { }

        /// <summary>
        /// </summary>
        public UnknownMessageTypeException(String message, Exception inner) : base(message, inner) { }
        
        /// <summary>
        /// </summary>
        protected UnknownMessageTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
