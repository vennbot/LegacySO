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
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Mina.Core.Session
{
    class DefaultIoSessionAttributeMap : IoSessionAttributeMap
    {
        private readonly ConcurrentDictionary<Object, Object> _attributes = new ConcurrentDictionary<Object, Object>();

        public Object GetAttribute(IoSession session, Object key, Object defaultValue)
        {
            if (defaultValue == null)
            {
                Object obj;
                _attributes.TryGetValue(key, out obj);
                return obj;
            }
            else
            {
                return _attributes.GetOrAdd(key, defaultValue);
            }
        }

        public Object SetAttribute(IoSession session, Object key, Object value)
        {
            Object old = null;
            _attributes.AddOrUpdate(key, value, (k, v) => 
            {
                old = v;
                return value;
            });
            return old;
        }

        public Object SetAttributeIfAbsent(IoSession session, Object key, Object value)
        {
            return _attributes.GetOrAdd(key, value);
        }

        public Object RemoveAttribute(IoSession session, Object key)
        {
            Object obj;
            _attributes.TryRemove(key, out obj);
            return obj;
        }

        public Boolean ContainsAttribute(IoSession session, Object key)
        {
            return _attributes.ContainsKey(key);
        }

        public IEnumerable<Object> GetAttributeKeys(IoSession session)
        {
            return _attributes.Keys;
        }

        public void Dispose(IoSession session)
        {
            // Do nothing
        }
    }
}
