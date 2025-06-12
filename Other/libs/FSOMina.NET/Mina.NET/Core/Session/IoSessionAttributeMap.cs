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

namespace Mina.Core.Session
{
    /// <summary>
    /// Stores the user-defined attributes which is provided per <see cref="IoSession"/>.
    /// All user-defined attribute accesses in <see cref="IoSession"/> are forwarded to
    /// the instance of <see cref="IoSessionAttributeMap"/>.
    /// </summary>
    public interface IoSessionAttributeMap
    {
        /// <summary>
        /// Returns the value of user defined attribute associated with the
        /// specified key. If there's no such attribute, the specified default
        /// value is associated with the specified key, and the default value is
        /// returned.
        /// </summary>
        Object GetAttribute(IoSession session, Object key, Object defaultValue);
        /// <summary>
        /// Sets a user-defined attribute.
        /// </summary>
        Object SetAttribute(IoSession session, Object key, Object value);
        /// <summary>
        /// Sets a user defined attribute if the attribute with the specified key
        /// is not set yet.
        /// </summary>
        Object SetAttributeIfAbsent(IoSession session, Object key, Object value);
        /// <summary>
        /// Removes a user-defined attribute with the specified key.
        /// </summary>
        Object RemoveAttribute(IoSession session, Object key);
        /// <summary>
        /// Returns <tt>true</tt> if this session contains the attribute with the specified <tt>key</tt>.
        /// </summary>
        Boolean ContainsAttribute(IoSession session, Object key);
        /// <summary>
        /// Returns the keys of all user-defined attributes.
        /// </summary>
        IEnumerable<Object> GetAttributeKeys(IoSession session);
        /// <summary>
        /// Disposes any releases associated with the specified session.
        /// This method is invoked on disconnection.
        /// </summary>
        void Dispose(IoSession session);
    }
}
