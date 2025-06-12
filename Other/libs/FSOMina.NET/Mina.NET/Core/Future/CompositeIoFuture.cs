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
using System.Threading;

namespace Mina.Core.Future
{
    /// <summary>
    /// An <see cref="IoFuture"/> of <see cref="IoFuture"/>s.
    /// It is useful when you want to get notified when all <see cref="IoFuture"/>s are complete.
    /// </summary>
    public class CompositeIoFuture<TFuture> : DefaultIoFuture
        where TFuture : IoFuture
    {
        private Int32 _unnotified;
        private volatile Boolean _constructionFinished;

        /// <summary>
        /// </summary>
        public CompositeIoFuture(IEnumerable<TFuture> children)
            : base(null)
        {
            foreach (TFuture f in children)
            {
                f.Complete += OnComplete;
                Interlocked.Increment(ref _unnotified);
            }

            _constructionFinished = true;
            if (_unnotified == 0)
                Value = true;
        }

        private void OnComplete(Object sender, IoFutureEventArgs e)
        {
            if (Interlocked.Decrement(ref _unnotified) == 0 && _constructionFinished)
                Value = true;
        }
    }
}
