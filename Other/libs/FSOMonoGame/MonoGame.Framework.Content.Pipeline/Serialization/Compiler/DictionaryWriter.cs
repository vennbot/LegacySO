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
    /// Writes the dictionary to the output.
    /// </summary>
    [ContentTypeWriter]
    class DictionaryWriter<K,V> : BuiltInContentWriter<Dictionary<K,V>>
    {
        ContentTypeWriter _keyWriter;
        ContentTypeWriter _valueWriter;

        /// <inheritdoc/>
        internal override void OnAddedToContentWriter(ContentWriter output)
        {
            base.OnAddedToContentWriter(output);

            _keyWriter = output.GetTypeWriter(typeof(K));
            _valueWriter = output.GetTypeWriter(typeof(V));
        }

        public override bool CanDeserializeIntoExistingObject
        {
            get { return true; }
        }

        protected internal override void Write(ContentWriter output, Dictionary<K,V> value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            output.Write(value.Count);
            foreach (var element in value)
            {
                output.WriteObject(element.Key, _keyWriter);
                output.WriteObject(element.Value, _valueWriter);
            }
        }
    }
}
