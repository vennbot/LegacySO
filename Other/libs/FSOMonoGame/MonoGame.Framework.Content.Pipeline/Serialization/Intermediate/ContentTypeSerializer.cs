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

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate
{
    public abstract class ContentTypeSerializer
    {
        protected ContentTypeSerializer(Type targetType, string xmlTypeName)
        {
            TargetType = targetType;
            XmlTypeName = xmlTypeName;
        }

        public virtual bool CanDeserializeIntoExistingObject
        {
            get { return false; }
        }

        public Type TargetType { get; private set; }

        public string XmlTypeName { get; private set; }

        protected internal abstract object Deserialize(IntermediateReader input, ContentSerializerAttribute format, object existingInstance);

        protected internal virtual void Initialize(IntermediateSerializer serializer)
        {     
        }

        public virtual bool ObjectIsEmpty(object value)
        {
            return false;
        }

        protected internal virtual void ScanChildren(IntermediateSerializer serializer, ChildCallback callback, object value)
        {
        }

        protected internal abstract void Serialize(IntermediateWriter output, object value, ContentSerializerAttribute format);

        internal protected delegate void ChildCallback(ContentTypeSerializer typeSerializer, object value);
    }
}
