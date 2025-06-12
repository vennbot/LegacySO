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

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate
{
    class ArraySerializer<T> : ContentTypeSerializer<T[]>
    {
        private readonly ListSerializer<T> _listSerializer;

        public ArraySerializer() :
            base("array")
        {
            _listSerializer = new ListSerializer<T>();
        }

        protected internal override void Initialize(IntermediateSerializer serializer)
        {
            _listSerializer.Initialize(serializer);
        }

        public override bool ObjectIsEmpty(T[] value)
        {
            return value.Length == 0;
        }

        protected internal override void ScanChildren(IntermediateSerializer serializer, ChildCallback callback, T[] value)
        {
            _listSerializer.ScanChildren(serializer, callback, new List<T>(value));
        }

        protected internal override T[] Deserialize(IntermediateReader input, ContentSerializerAttribute format, T[] existingInstance)
        {
            if (existingInstance != null)
                throw new InvalidOperationException("You cannot deserialize an array into a getter-only property.");
            var result = _listSerializer.Deserialize(input, format, null);
            return result.ToArray();
        }

        protected internal override void Serialize(IntermediateWriter output, T[] value, ContentSerializerAttribute format)
        {
            _listSerializer.Serialize(output, new List<T>(value), format);
        }
    }
}
