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
 using System.Security;

namespace MonoGame.Tools.Pipeline
{
    public static class StringExtensions
    {
        public static string Unescape(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var result = Uri.UnescapeDataString(text);

            // JCF: XmlReader already does this.
            /*
            result = result.Replace("&apos;", "'");
            result = result.Replace("&quot;", "\"");
            result = result.Replace("&gt;", ">");
            result = result.Replace("&amp;", "&");
            */             
            
            return result;
        }
    }
}
