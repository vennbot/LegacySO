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

namespace Microsoft.Xna.Framework.Content
{
    /// <summary>
    /// This is used to specify the type to use when deserializing this object at runtime.
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
    public sealed class ContentSerializerRuntimeTypeAttribute : Attribute
    {
        /// <summary>
        /// Creates an instance of the attribute.
        /// </summary>
        /// <param name="runtimeType">The name of the type to use at runtime.</param>
        public ContentSerializerRuntimeTypeAttribute(string runtimeType)
        {
            RuntimeType = runtimeType;
        }

        /// <summary>
        /// The name of the type to use at runtime.
        /// </summary>
        public string RuntimeType { get; private set;}
    }
}

