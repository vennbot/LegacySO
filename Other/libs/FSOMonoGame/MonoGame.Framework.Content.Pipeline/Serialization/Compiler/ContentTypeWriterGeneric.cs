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

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler
{
    /// <summary>
    /// Provides a generic implementation of ContentTypeWriter methods and properties for compiling a specific managed type into a binary format.
    /// </summary>
    /// <typeparam name="T">The type to write</typeparam>
    /// <remarks>This is a generic implementation of ContentTypeWriter and, therefore, can handle strongly typed content data.</remarks>
    public abstract class ContentTypeWriter<T> : ContentTypeWriter
    {
        /// <summary>
        /// Initializes a new instance of the ContentTypeWriter class.
        /// </summary>
        protected ContentTypeWriter()
            : base(typeof(T))
        {
        }

        /// <summary>
        /// Compiles a strongly typed object into binary format.
        /// </summary>
        /// <param name="output">The content writer serializing the value.</param>
        /// <param name="value">The value to write.</param>
        protected internal override void Write(ContentWriter output, object value)
        {
            Write(output, (T)value);
        }

        /// <summary>
        /// Compiles a strongly typed object into binary format.
        /// </summary>
        /// <param name="output">The content writer serializing the value.</param>
        /// <param name="value">The value to write.</param>
        protected internal abstract void Write(ContentWriter output, T value);
    }
}
