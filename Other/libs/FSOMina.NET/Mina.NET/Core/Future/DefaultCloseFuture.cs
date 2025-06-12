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
using Mina.Core.Session;

namespace Mina.Core.Future
{
    /// <summary>
    /// A default implementation of <see cref="ICloseFuture"/>.
    /// </summary>
    public class DefaultCloseFuture : DefaultIoFuture, ICloseFuture
    {
        /// <summary>
        /// </summary>
        public DefaultCloseFuture(IoSession session)
            : base(session)
        { }

        /// <inheritdoc/>
        public Boolean Closed
        {
            get
            {
                if (Done)
                {
                    Object v = Value;
                    if (v is Boolean)
                        return (Boolean)v;
                }
                return false;
            }
            set { Value = true; }
        }

        /// <inheritdoc/>
        public new ICloseFuture Await()
        {
            return (ICloseFuture)base.Await();
        }
    }
}
