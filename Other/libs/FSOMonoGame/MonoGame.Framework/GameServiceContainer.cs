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
// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Utilities;

namespace Microsoft.Xna.Framework
{
    public class GameServiceContainer : IServiceProvider
    {
        Dictionary<Type, object> services;

        public GameServiceContainer()
        {
            services = new Dictionary<Type, object>();
        }

        public void AddService(Type type, object provider)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (!ReflectionHelpers.IsAssignableFrom(type, provider))
                throw new ArgumentException("The provider does not match the specified service type!");

            services.Add(type, provider);
        }

        public object GetService(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
						
            object service;
            if (services.TryGetValue(type, out service))
                return service;

            return null;
        }

        public void RemoveService(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            services.Remove(type);
        }
        
        public void AddService<T>(T provider)
        {
            AddService(typeof(T), provider);
        }

 	public T GetService<T>() where T : class
        {
            var service = GetService(typeof(T));

            if (service == null)
                return null;

            return (T)service;
        }
    }
}
