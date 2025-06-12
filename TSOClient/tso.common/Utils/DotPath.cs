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
using System.Reflection;

namespace FSO.Common.Utils
{
    public static class DotPath
    {
        public static PropertyInfo[] CompileDotPath(Type sourceType, string sourcePath)
        {
            //Dot path
            var path = sourcePath.Split(new char[] { '.' });
            var properties = new PropertyInfo[path.Length];

            var currentType = sourceType;
            for (int i = 0; i < path.Length; i++)
            {
                var property = currentType.GetProperty(path[i]);
                properties[i] = property;
                currentType = property.PropertyType;
            }

            return properties;
        }

        public static object GetDotPathValue(object source, PropertyInfo[] path)
        {
            if (source == null) { return null; }

            var currentValue = source;
            for (var i = 0; i < path.Length; i++)
            {
                currentValue = path[i].GetValue(currentValue, null);
                if (currentValue == null) { return null; }
            }

            return currentValue;
        }

        public static void SetDotPathValue(object source, PropertyInfo[] path, object value)
        {
            if (source == null) { return; }

            var currentValue = source;
            for (var i = 0; i < path.Length - 1; i++)
            {
                currentValue = path[i].GetValue(currentValue, null);
                if (currentValue == null) { return; }
            }

            var member = path[path.Length - 1];
            member.SetValue(currentValue, value, null);
        }
    }
}
