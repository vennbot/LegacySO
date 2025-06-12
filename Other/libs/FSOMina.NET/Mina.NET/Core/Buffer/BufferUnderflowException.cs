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

namespace Mina.Core.Buffer
{
    /// <summary>
    /// An exception thrown when a <code>Get</code> operation reaches the source buffer's limit.
    /// </summary>
    [Serializable]
    public class BufferUnderflowException : Exception
    {
        /// <summary>
        /// </summary>
        public BufferUnderflowException() { }

        /// <summary>
        /// </summary>
        public BufferUnderflowException(String message)
            : base(message) { }

        /// <summary>
        /// </summary>
        public BufferUnderflowException(String message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// </summary>
        protected BufferUnderflowException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// </summary>
        public override void GetObjectData(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
