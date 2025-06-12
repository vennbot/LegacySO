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

namespace Mina.Core.Write
{
    /// <summary>
    /// An exception which is thrown when one or more write operations were
    /// attempted on a closed session.
    /// </summary>
    [Serializable]
    public class WriteToClosedSessionException : WriteException
    {
        /// <summary>
        /// </summary>
        public WriteToClosedSessionException(IWriteRequest request)
            : base(request)
        { }

        /// <summary>
        /// </summary>
        public WriteToClosedSessionException(IEnumerable<IWriteRequest> requests)
            : base(requests)
        { }

        /// <summary>
        /// </summary>
        protected WriteToClosedSessionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
