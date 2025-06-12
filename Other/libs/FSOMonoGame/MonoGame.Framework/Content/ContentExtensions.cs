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
using System.Linq;

namespace Microsoft.Xna.Framework.Content
{
    internal static class ContentExtensions
    {
        public static ConstructorInfo GetDefaultConstructor(this Type type)
        {
#if NET45
            var typeInfo = type.GetTypeInfo();
            var ctor = typeInfo.DeclaredConstructors.FirstOrDefault(c => !c.IsStatic && c.GetParameters().Length == 0);
            return ctor;
#else
            var attrs = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            return type.GetConstructor(attrs, null, new Type[0], null);
#endif
        }

        public static PropertyInfo[] GetAllProperties(this Type type)
        {

            // Sometimes, overridden properties of abstract classes can show up even with 
            // BindingFlags.DeclaredOnly is passed to GetProperties. Make sure that
            // all properties in this list are defined in this class by comparing
            // its get method with that of it's base class. If they're the same
            // Then it's an overridden property.
#if NET45
            PropertyInfo[] infos= type.GetTypeInfo().DeclaredProperties.ToArray();
            var nonStaticPropertyInfos = from p in infos
                                         where (p.GetMethod != null) && (!p.GetMethod.IsStatic) &&
                                         (p.GetMethod == p.GetMethod.GetRuntimeBaseDefinition())
                                         select p;
            return nonStaticPropertyInfos.ToArray();
#else
            const BindingFlags attrs = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            var allProps = type.GetProperties(attrs).ToList();
            var props = allProps.FindAll(p => p.GetGetMethod(true) != null && p.GetGetMethod(true) == p.GetGetMethod(true).GetBaseDefinition()).ToArray();
            return props;
#endif
        }


        public static FieldInfo[] GetAllFields(this Type type)
        {
#if NET45
            FieldInfo[] fields= type.GetTypeInfo().DeclaredFields.ToArray();
            var nonStaticFields = from field in fields
                    where !field.IsStatic
                    select field;
            return nonStaticFields.ToArray();
#else
            var attrs = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            return type.GetFields(attrs);
#endif
        }

        public static bool IsClass(this Type type)
        {
#if NET45
            return type.GetTypeInfo().IsClass;
#else
            return type.IsClass;
#endif
        }
    }
}
