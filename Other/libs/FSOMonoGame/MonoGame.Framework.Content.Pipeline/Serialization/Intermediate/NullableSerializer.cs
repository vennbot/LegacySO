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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate
{
    [ContentTypeSerializer]
    class NullableSerializer<T> : ContentTypeSerializer<T?> where T : struct
    {
        private ContentTypeSerializer _serializer;
        private ContentSerializerAttribute _format;

        protected internal override void Initialize(IntermediateSerializer serializer)
        {
            _serializer = serializer.GetTypeSerializer(typeof(T));
            _format = new ContentSerializerAttribute
            {
                FlattenContent = true
            };
        }

        protected internal override T? Deserialize(IntermediateReader input, ContentSerializerAttribute format, T? existingInstance)
        {
            return input.ReadRawObject<T>(_format, _serializer);
        }

        protected internal override void Serialize(IntermediateWriter output, T? value, ContentSerializerAttribute format)
        {
            output.WriteRawObject<T>(value.Value, _format, _serializer);
        }
    }
}
