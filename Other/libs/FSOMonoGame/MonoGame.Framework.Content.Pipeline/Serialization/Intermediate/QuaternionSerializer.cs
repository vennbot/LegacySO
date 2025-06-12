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

using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate
{
    [ContentTypeSerializer]
    class QuaternionSerializer : ElementSerializer<Quaternion>
    {
        public QuaternionSerializer() :
            base("Quaternion", 4)
        {
        }

        protected internal override Quaternion Deserialize(string[] inputs, ref int index)
        {
            return new Quaternion(  XmlConvert.ToSingle(inputs[index++]),
                                    XmlConvert.ToSingle(inputs[index++]),
                                    XmlConvert.ToSingle(inputs[index++]),
                                    XmlConvert.ToSingle(inputs[index++]));
        }

        protected internal override void Serialize(Quaternion value, List<string> results)
        {
            results.Add(XmlConvert.ToString(value.X));
            results.Add(XmlConvert.ToString(value.Y));
            results.Add(XmlConvert.ToString(value.Z));
            results.Add(XmlConvert.ToString(value.W));
        }
    }
}
