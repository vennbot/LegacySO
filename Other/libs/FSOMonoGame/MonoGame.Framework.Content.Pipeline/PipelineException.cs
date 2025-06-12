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
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework.Content.Pipeline
{
    /// <summary>
    /// Thrown when errors are encountered during a content pipeline build.
    /// </summary>
    [SerializableAttribute]
    public class PipelineException : Exception
    {
        /// <summary>
        /// Creates an instance of PipelineException.
        /// </summary>
        public PipelineException()
        {
        }

        /// <summary>
        /// Creates an instance of PipelineException with information on serialization and streaming context for the related content item.
        /// </summary>
        /// <param name="serializationInfo">Information necessary for serialization and deserialization of the content item.</param>
        /// <param name="streamingContext">Information necessary for the source and destination of a given serialized stream. Also provides an additional caller-defined context.</param>
        protected PipelineException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext
            )
        {
        }

        /// <summary>
        /// Initializes a new instance of the PipelineException class with the specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public PipelineException(
            string message
            )
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PipelineException class with the specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If innerException is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public PipelineException(
            string message,
            Exception innerException
            )
            :base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PipelineException class with the specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="messageArgs">Array of strings specifying message-related arguments.</param>
        public PipelineException(
            string message,
            params Object[] messageArgs
            )
            : base(String.Format(message, messageArgs))
        {
        }
    }
}
