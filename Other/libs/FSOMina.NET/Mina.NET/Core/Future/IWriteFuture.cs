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

namespace Mina.Core.Future
{
    /// <summary>
    /// An <see cref="IoFuture"/> for asynchronous write requests.
    /// </summary>
    public interface IWriteFuture : IoFuture
    {
        /// <summary>
        /// Gets or sets a value indicating if this write operation is finished successfully.
        /// </summary>
        Boolean Written { get; set; }
        /// <summary>
        /// Gets or sets the cause of the write failure if and only if the write
        /// operation has failed due to an <see cref="Exception"/>.
        /// Otherwise null is returned.
        /// </summary>
        Exception Exception { get; set; }
        /// <inheritdoc/>
        new IWriteFuture Await();
    }
}
