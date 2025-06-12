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
using System.Globalization;
using System.Xml;

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate
{
    [ContentTypeSerializer]
    class ColorSerializer : ElementSerializer<Color>
    {
        public ColorSerializer() :
            base("Color", 1)
        {
        }

        protected internal override Color Deserialize(string[] inputs, ref int index)
        {
            // NOTE: The value is serialized in ARGB format.
            var value = uint.Parse(inputs[index++], NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            return new Color(   (int)(value >> 16 & 0xFF),
                                (int)(value >> 8 & 0xFF),
                                (int)(value >> 0 & 0xFF),
                                (int)(value >> 24 & 0xFF));
        }

        protected internal override void Serialize(Color value, List<string> results)
        {
            // NOTE: The value is serialized in ARGB format.
            results.Add(string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", value.A, value.R, value.G, value.B));
        }
    }
}
