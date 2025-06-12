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
using System.IO;

namespace Mina.Core.Write
{
    /// <summary>
    /// An exception which is thrown when one or more write operations were failed.
    /// </summary>
    [Serializable]
    public class WriteException : IOException
    {
        private readonly IList<IWriteRequest> _requests;

        public WriteException(IWriteRequest request)
        {
            _requests = AsRequestList(request);
        }

        public WriteException(IEnumerable<IWriteRequest> requests)
        {
            _requests = AsRequestList(requests);
        }

        protected WriteException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public IWriteRequest Request
        {
            get { return _requests[0]; }
        }

        public IEnumerable<IWriteRequest> Requests
        {
            get { return _requests; }
        }

        /// <inheritdoc/>
        public override void GetObjectData(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        private static IList<IWriteRequest> AsRequestList(IWriteRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            List<IWriteRequest> requests = new List<IWriteRequest>(1);
            requests.Add(request);
            return requests.AsReadOnly();
        }

        private static IList<IWriteRequest> AsRequestList(IEnumerable<IWriteRequest> requests)
        {
            if (requests == null)
                throw new ArgumentNullException("requests");
            List<IWriteRequest> newRequests = new List<IWriteRequest>(requests);
            return newRequests.AsReadOnly();
        }
    }
}
