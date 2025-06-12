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

namespace Mina.Core.Session
{
    /// <summary>
    /// Creates a Key from a class name and an attribute name. The resulting Key will
    /// be stored in the session Map.
    /// </summary>
    [Serializable]
    public sealed class AttributeKey
    {
        private readonly String _name;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="source">the class this AttributeKey will be attached to</param>
        /// <param name="name">the Attribute name</param>
        public AttributeKey(Type source, String name)
        {
            _name = source.Name + "." + name + "@" + base.GetHashCode().ToString("X");
        }

        /// <inheritdoc/>
        public override String ToString()
        {
            return _name;
        }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            int h = 17 * 37 + ((_name == null) ? 0 : _name.GetHashCode());
            return h;
        }

        /// <inheritdoc/>
        public override Boolean Equals(Object obj)
        {
            if (Object.ReferenceEquals(this, obj))
                return true;
            AttributeKey other = obj as AttributeKey;
            if (other == null)
                return false;
            return _name.Equals(other._name);
        }
    }
}
