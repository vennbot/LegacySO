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
using System.Diagnostics;

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate
{
    class EnumSerializer : ContentTypeSerializer
    {
        public EnumSerializer(Type targetType) :
            base(targetType, targetType.Name)
        {
        }

        protected internal override object Deserialize(IntermediateReader input, ContentSerializerAttribute format, object existingInstance)
        {
            var str = input.Xml.ReadString();
            try
            {
                return Enum.Parse(TargetType, str, true);
            }
            catch (Exception ex)
            {
                throw input.NewInvalidContentException(ex, "Invalid enum value '{0}' for type '{1}'", str, TargetType.Name);
            }
        }

        protected internal override void Serialize(IntermediateWriter output, object value, ContentSerializerAttribute format)
        {
            Debug.Assert(value.GetType() == TargetType, "Got invalid value type!");
            output.Xml.WriteString(value.ToString());
        }
    }
}
