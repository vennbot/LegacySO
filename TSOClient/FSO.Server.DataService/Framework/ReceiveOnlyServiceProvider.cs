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
using System.Threading.Tasks;

namespace FSO.Common.DataService.Framework
{
    public abstract class ReceiveOnlyServiceProvider<KEY, VALUE> : AbstractDataServiceProvider<KEY, VALUE> where VALUE : IModel
    {
        //protected Dictionary<KEY, VALUE> Items = new Dictionary<KEY, VALUE>();
        protected Dictionary<KEY, Task<object>> Values = new Dictionary<KEY, Task<object>>();
        protected TimeSpan LazyLoadTimeout = TimeSpan.FromSeconds(10);

        public override Task<object> Get(object key)
        {
            if (!(key is KEY))
            {
                throw new Exception("Key must be of type " + typeof(KEY));
            }

            var castKey = (KEY)key;

            if (Values.ContainsKey(castKey))
            {
                return Values[castKey];
            }

            lock (Values)
            {
                if (Values.ContainsKey(castKey))
                {
                    return Values[castKey];
                }

                var result = ResolveMissingKey(castKey);
                Values.Add(castKey, result);
                return result;
            }
        }

        private Task<object> ResolveMissingKey(object key)
        {
            var cts = new CancellationTokenSource(LazyLoadTimeout);
            return Task.Factory.StartNew<object>(() =>
            {
                return (object)CreateInstance((KEY)key);
            }, cts.Token);
        }

        protected virtual VALUE CreateInstance(KEY key)
        {
            return ModelActivator.NewInstance<VALUE>();
        }
    }
}
