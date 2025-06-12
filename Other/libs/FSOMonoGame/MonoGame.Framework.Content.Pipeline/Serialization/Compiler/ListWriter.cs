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
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler
{
    /// <summary>
    /// Writes the list to the output.
    /// </summary>
    [ContentTypeWriter]
    class ListWriter<T> : BuiltInContentWriter<List<T>>
    {
        ContentTypeWriter _elementWriter;

        /// <inheritdoc/>
        internal override void OnAddedToContentWriter(ContentWriter output)
        {
            base.OnAddedToContentWriter(output);

            _elementWriter = output.GetTypeWriter(typeof(T));
        }

        public override bool CanDeserializeIntoExistingObject
        {
            get { return true; }
        }

        /// <summary>
        /// Writes the value to the output.
        /// </summary>
        /// <param name="output">The output writer object.</param>
        /// <param name="value">The value to write to the output.</param>
        protected internal override void Write(ContentWriter output, List<T> value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            output.Write(value.Count);
            foreach (var element in value)
            {
                output.WriteObject(element, _elementWriter);
            }
        }
    }
}
