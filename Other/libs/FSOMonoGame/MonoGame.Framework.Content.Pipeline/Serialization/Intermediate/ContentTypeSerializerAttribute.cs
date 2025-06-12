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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate
{
    /// <summary>
    /// Used to identify custom ContentTypeSerializer classes. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ContentTypeSerializerAttribute : Attribute
    {
        /// <summary>
        /// Initializes an instance of the ContentTypeSerializerAttribute.
        /// </summary>
        public ContentTypeSerializerAttribute()
        {
        }


        private static readonly object _lock = new object();

        private static ReadOnlyCollection<Type> _types;

        static internal ReadOnlyCollection<Type> GetTypes()
        {
            lock (_lock)
            {
                if (_types == null)
                {
                    var found = new List<Type>();
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (var assembly in assemblies)
                    {
                        try
                        {
                            var types = assembly.GetTypes();
                            foreach (var type in types)
                            {
                                var attributes = type.GetCustomAttributes(typeof (ContentTypeSerializerAttribute), false);
                                if (attributes.Length > 0)
                                    found.Add(type);
                            }
                        }
                        catch (System.Reflection.ReflectionTypeLoadException ex)
                        {
                            Console.WriteLine("Warning: " + ex.Message);
                        }
                    }

                    _types = new ReadOnlyCollection<Type>(found);
                }
            }

            return _types;
        }
    }
}
