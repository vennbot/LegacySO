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
using System.Collections.Immutable;
using System.Linq;

namespace FSO.Common.DataService.Framework
{
    public class ModelActivator
    {
        public static T NewInstance<T>() where T : IModel
        {
            return (T)NewInstance(typeof(T));
        }

        public static object NewInstance(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ImmutableList<>))
            {
                var listType = typeof(ImmutableList);
                var consMethod = listType.GetMethods().Where(method => method.IsGenericMethod).FirstOrDefault();
                var generic = consMethod.MakeGenericMethod(type.GenericTypeArguments[0]);

                return generic.Invoke(null, new object[] { }); 
            }
            var instance = Activator.CreateInstance(type);
            var properties = type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            
            foreach (var property in properties)
            {
                if (property.GetIndexParameters().Length > 0) { continue; }
                var propertyType = property.PropertyType;

                if (!IsBasicType(propertyType))
                {
                    //Need to activate child members too
                    property.SetValue(instance, NewInstance(propertyType), null);
                }
            }

            return instance;
        }

        public static bool IsBasicType(Type type)
        {
            return type.IsPrimitive ||
                   type.IsAssignableFrom(typeof(string)) ||
                   type.IsEnum;
        }
    }
}
