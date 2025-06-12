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
    class MatrixSerializer : ElementSerializer<Matrix>
    {
        public MatrixSerializer() :
            base("Matrix", 16)
        {
        }

        protected internal override Matrix Deserialize(string[] inputs, ref int index)
        {
            return new Matrix(XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]),
                                XmlConvert.ToSingle(inputs[index++]));
        }

        protected internal override void Serialize(Matrix value, List<string> results)
        {
            results.Add(XmlConvert.ToString(value.M11));
            results.Add(XmlConvert.ToString(value.M12));
            results.Add(XmlConvert.ToString(value.M13));
            results.Add(XmlConvert.ToString(value.M14));
            results.Add(XmlConvert.ToString(value.M21));
            results.Add(XmlConvert.ToString(value.M22));
            results.Add(XmlConvert.ToString(value.M23));
            results.Add(XmlConvert.ToString(value.M24));
            results.Add(XmlConvert.ToString(value.M31));
            results.Add(XmlConvert.ToString(value.M32));
            results.Add(XmlConvert.ToString(value.M33));
            results.Add(XmlConvert.ToString(value.M34));
            results.Add(XmlConvert.ToString(value.M41));
            results.Add(XmlConvert.ToString(value.M42));
            results.Add(XmlConvert.ToString(value.M43));
            results.Add(XmlConvert.ToString(value.M44));
        }
    }
}
