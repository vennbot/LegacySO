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

namespace FSO.Client.UI.Framework.Parser
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class UIAttribute : System.Attribute
    {
        public string Name { get; set; }
        public Type Parser { get; set; }
        public UIAttributeType DataType = UIAttributeType.Unknown;

        public UIAttribute(string name)
        {
            this.Name = name;
        }

        public UIAttribute(string name, Type parser)
        {
            this.Name = name;
            this.Parser = parser;
        }
    }

    public interface UIAttributeParser
    {
        void ParseAttribute(UINode node);
    }

}
