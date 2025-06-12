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
using System.Globalization;
using Microsoft.Xna.Framework;

namespace TwoMGFX.TPGParser
{
	public static class ParseTreeTools
	{
        public static float ParseFloat(string value)
        {
            // Remove all whitespace and trailing F or f.
            value = value.Replace(" ", "");
            value = value.TrimEnd('f', 'F');
            return float.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        public static int ParseInt(string value)
        {
            // We read it as a float and cast it down to
            // an integer to match Microsoft FX behavior.
            return (int)Math.Floor(ParseFloat(value));
        }
       
		public static bool ParseBool(string value)
		{
		    if (value.ToLowerInvariant() == "true" || value == "1")
				return true;
		    if (value.ToLowerInvariant() == "false" || value == "0")
		        return false;

		    throw new Exception("Invalid boolean value '" + value + "'");
		}

	    public static Color ParseColor(string value)
	    {
	        var hexValue = Convert.ToUInt32(value, 16);

	        byte r, g, b, a;
	        if (value.Length == 8)
	        {
	            r = (byte) ((hexValue >> 16) & 0xFF);
                g = (byte) ((hexValue >> 8) & 0xFF);
                b = (byte) ((hexValue >> 0) & 0xFF);
	            a = 255;
	        }
	        else if (value.Length == 10)
	        {
                r = (byte) ((hexValue >> 24) & 0xFF);
                g = (byte) ((hexValue >> 16) & 0xFF);
                b = (byte) ((hexValue >> 8) & 0xFF);
                a = (byte) ((hexValue >> 0) & 0xFF);
	        }
	        else
	        {
	            throw new NotSupportedException();
	        }

            return new Color(r, g, b, a);
	    }        
    }
}
