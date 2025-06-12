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
    /// A default immutable implementation of <see cref="ITransportMetadata"/>.
    /// </summary>
    public class DefaultTransportMetadata : ITransportMetadata
    {
        private readonly String _providerName;
        private readonly String _name;
        private readonly Boolean _connectionless;
        private readonly Boolean _hasFragmentation;
        private readonly Type _endpointType;

        /// <summary>
        /// </summary>
        public DefaultTransportMetadata(String providerName, String name,
            Boolean connectionless, Boolean fragmentation, Type endpointType)
        {
            if (providerName == null)
                throw new ArgumentNullException("providerName");
            if (name == null)
                throw new ArgumentNullException("name");

            providerName = providerName.Trim().ToLowerInvariant();
            if (providerName.Length == 0)
                throw new ArgumentException("providerName is empty", "providerName");
            name = name.Trim().ToLowerInvariant();
            if (name.Length == 0)
                throw new ArgumentException("name is empty", "name");

            if (endpointType == null)
                throw new ArgumentNullException("endpointType");

            _providerName = providerName;
            _name = name;
            _connectionless = connectionless;
            _hasFragmentation = fragmentation;
            _endpointType = endpointType;
        }

        /// <inheritdoc/>
        public String ProviderName
        {
            get { return _providerName; }
        }

        /// <inheritdoc/>
        public String Name
        {
            get { return _name; }
        }

        /// <inheritdoc/>
        public Boolean Connectionless
        {
            get { return _connectionless; }
        }

        /// <inheritdoc/>
        public Boolean HasFragmentation
        {
            get { return _hasFragmentation; }
        }

        /// <inheritdoc/>
        public Type EndPointType
        {
            get { return _endpointType; }
        }

        /// <inheritdoc/>
        public override String ToString()
        {
            return _name;
        }
    }
}
