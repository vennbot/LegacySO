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

namespace Microsoft.Xna.Framework.GamerServices
{
    public class PropertyDictionary
    {
        private Dictionary<string,object> PropDictionary = new Dictionary<string, object>();
        public int GetValueInt32 (string aKey)
        {
            return (int)PropDictionary[aKey];
        }

        public DateTime GetValueDateTime (string aKey)
        {
            return (DateTime)PropDictionary[aKey];
        }

        public void SetValue(string aKey, DateTime aValue)
        {
            if(PropDictionary.ContainsKey(aKey))
            {
                PropDictionary[aKey] = aValue;
            }
            else
            {
                PropDictionary.Add(aKey,aValue);
            }
        }
    }

}

