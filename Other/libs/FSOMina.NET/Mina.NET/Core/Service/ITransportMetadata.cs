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

namespace Mina.Core.Service
{
    /// <summary>
    /// Provides meta-information that describes an <see cref="IoService"/>.
    /// </summary>
    public interface ITransportMetadata
    {
        /// <summary>
        /// Gets the name of the service provider.
        /// </summary>
        String ProviderName { get; }
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        String Name { get; }
        /// <summary>
        /// Returns <code>true</code> if the session of this transport type is
        /// <a href="http://en.wikipedia.org/wiki/Connectionless">connectionless</a>.
        /// </summary>
        Boolean Connectionless { get; }
        /// <summary>
        /// Returns <code>true</code> if the messages exchanged by the service can be
        /// <a href="http://en.wikipedia.org/wiki/IPv4#Fragmentation_and_reassembly">fragmented
        /// or reassembled</a> by its underlying transport.
        /// </summary>
        Boolean HasFragmentation { get; }
        /// <summary>
        /// Gets the type of the endpoint in this transport.
        /// </summary>
        Type EndPointType { get; }
    }
}
