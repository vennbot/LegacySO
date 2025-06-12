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
    class CharSerializer : ElementSerializer<char>
    {
        public CharSerializer() :
            base("char", 1)
        {
        }

        protected internal override char Deserialize(string[] inputs, ref int index)
        {
            var str = inputs[index++];
            if (str.Length == 1)
                return XmlConvert.ToChar(str);

            // Try parsing it as a UTF code.
            int val;
            if (int.TryParse(str, out val))
                return char.ConvertFromUtf32(val)[0];

            // Last ditch effort to decode it as XML escape value.
            return XmlConvert.ToChar(XmlConvert.DecodeName(str));
        }

        protected internal override void Serialize(char value, List<string> results)
        {
            results.Add(XmlConvert.ToString(value));
        }
    }
}
