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

using System.Drawing;
using System.Xml.Serialization;

namespace MonoGame.Framework.Content.Pipeline.Builder
{
    /// <summary>
    /// Helper for serializing color types with the XmlSerializer.
    /// </summary>
    public class XmlColor
    {
        private Color _color;

        public XmlColor()
        {            
        }

        public XmlColor(Color c)
        {
            _color = c;
        }

        public static implicit operator Color(XmlColor x)
        {
            return x._color;
        }

        public static implicit operator XmlColor(Color c)
        {
            return new XmlColor(c);
        }

        public static string FromColor(Color color)
        {
            if (color.IsNamedColor)
                return color.Name;
            return string.Format("{0}, {1}, {2}, {3}", color.R, color.G, color.B, color.A);
        }

        public static Color ToColor(string value)
        {            
            if (!value.Contains(","))
                return Color.FromName(value);

            int r, g, b, a;
            var colors = value.Split(',');
            int.TryParse(colors.Length > 0 ? colors[0] : string.Empty, out r);
            int.TryParse(colors.Length > 1 ? colors[1] : string.Empty, out g);
            int.TryParse(colors.Length > 2 ? colors[2] : string.Empty, out b);
            int.TryParse(colors.Length > 3 ? colors[3] : string.Empty, out a);

            return Color.FromArgb(a, r, g, b);
        }

        [XmlText]
        public string Default
        {
            get { return FromColor(_color); }
            set { _color = ToColor(value); }
        }
    }
}
