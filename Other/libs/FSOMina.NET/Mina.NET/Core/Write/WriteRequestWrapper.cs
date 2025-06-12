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
using System.Net;
using Mina.Core.Future;

namespace Mina.Core.Write
{
    /// <summary>
    /// A wrapper for an existing <see cref="IWriteRequest"/>.
    /// </summary>
    public class WriteRequestWrapper : IWriteRequest
    {
        private readonly IWriteRequest _inner;

        /// <summary>
        /// </summary>
        public WriteRequestWrapper(IWriteRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            _inner = request;
        }

        /// <inheritdoc/>
        public IWriteRequest OriginalRequest
        {
            get { return _inner.OriginalRequest; }
        }

        /// <inheritdoc/>
        public virtual Object Message
        {
            get { return _inner.Message; }
        }

        /// <inheritdoc/>
        public EndPoint Destination
        {
            get { return _inner.Destination; }
        }

        /// <inheritdoc/>
        public Boolean Encoded
        {
            get { return _inner.Encoded; }
        }

        /// <inheritdoc/>
        public IWriteFuture Future
        {
            get { return _inner.Future; }
        }

        public IWriteRequest InnerRequest
        {
            get { return _inner; }
        }
    }
}
