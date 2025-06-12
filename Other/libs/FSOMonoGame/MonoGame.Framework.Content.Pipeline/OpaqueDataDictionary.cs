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

using System;

namespace Microsoft.Xna.Framework.Content.Pipeline
{
    [ContentSerializerCollectionItemName("Data")]
    public sealed class OpaqueDataDictionary : NamedValueDictionary<Object>
    {
        /// <summary>
        /// Get the value for the specified key
        /// </summary>
        /// <key>The key of the item to retrieve.</key>
        /// <defaultValue>The default value to return if the key does not exist.</defaultValue>
        /// <returns>The item cast as T, or the default value if the item is not present in the dictonary.</returns>
        public T GetValue<T> (string key, T defaultValue)
        {
            object o;
            if (TryGetValue (key, out o))
                return (T)o ;
            return defaultValue;
        }
    }
}
