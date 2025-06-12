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
using System.Reflection;

namespace FSO.Common.Utils
{
    /// <summary>
    /// Helps UI controls like lists refer to a data service value
    /// for labels such that when updates come in the labels update
    /// </summary>
    public class ValuePointer
    {
        private object Item;
        private PropertyInfo Field;

        public ValuePointer(object item, string field)
        {
            this.Item = item;
            this.Field = item.GetType().GetProperty(field);
        }

        public object Get()
        {
            return Field.GetValue(Item);
        }

        public override string ToString()
        {
            var value = Get();
            if(value != null)
            {
                return value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static T Get<T>(object value)
        {
            if(value == null)
            {
                return default(T);
            }

            if(value is ValuePointer)
            {
                return (T)((ValuePointer)value).Get();
            }

            return (T)value;
        }
    }
}
