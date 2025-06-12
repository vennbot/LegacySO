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
using System.Xml;

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate
{
    internal static class PackedElementsHelper
    {
        private static readonly char[] _seperators = { ' ', '\t', '\n' };

        private const string _writeSeperator = " ";

        internal static string[] ReadElements(IntermediateReader input)
        {
            if (input.Xml.IsEmptyElement)
                return new string[0];

            string str = string.Empty;
            while (input.Xml.NodeType != XmlNodeType.EndElement)
            {
                if (input.Xml.NodeType == XmlNodeType.Comment)
                    input.Xml.Read();
                else
                    str += input.Xml.ReadString();
            }

            // Special case for char ' '
            if (str.Length > 0 && str.Trim() == string.Empty)
                return new string[] { str };

            var elements = str.Split(_seperators, StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length == 1 && string.IsNullOrEmpty(elements[0]))
                return new string[0];

            return elements;
        }

        public static string JoinElements(IEnumerable<string> elements)
        {
            return string.Join(_writeSeperator, elements);
        }
    }
}
