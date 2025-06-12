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

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler
{
    /// <summary>
    /// Writes the enum value to the output. Usually 32 bit, but can be other sizes if T is not integer.
    /// </summary>
    /// <typeparam name="T">The enum type to write.</typeparam>
    [ContentTypeWriter]
    class EnumWriter<T> : BuiltInContentWriter<T>
    {
        Type _underlyingType;
        ContentTypeWriter _underlyingTypeWriter;

        /// <inheritdoc/>
        internal override void OnAddedToContentWriter(ContentWriter output)
        {
            base.OnAddedToContentWriter(output);
            _underlyingType = Enum.GetUnderlyingType(typeof(T));
            _underlyingTypeWriter = output.GetTypeWriter(_underlyingType);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Microsoft.Xna.Framework.Content.EnumReader`1[[" + GetRuntimeType(targetPlatform) + "]]";
        }

        protected internal override void Write(ContentWriter output, T value)
        {
            output.WriteRawObject(Convert.ChangeType(value, _underlyingType), _underlyingTypeWriter);
        }
    }
}
