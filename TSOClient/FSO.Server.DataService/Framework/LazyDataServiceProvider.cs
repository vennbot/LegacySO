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
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace FSO.Common.DataService.Framework
{
    public abstract class LazyDataServiceProvider <KEY, VALUE> : AbstractDataServiceProvider<KEY, VALUE> where VALUE : IModel
    {
        //protected Dictionary<KEY, VALUE> Items = new Dictionary<KEY, VALUE>();
        protected ConcurrentDictionary<KEY, TaskWrap<object>> Values = new ConcurrentDictionary<KEY, TaskWrap<object>>();
        protected TimeSpan LazyLoadTimeout = TimeSpan.FromSeconds(10);

        public Task<object> ReloadTest(KEY castKey)
        {
            var val = Values[castKey];
            if (val.Ready && RequiresReload(castKey, (VALUE)val.GetReady())) return null;
            else return Values[castKey].Get();
        }

        public override Task<object> Get(object key)
        {
            if (!(key is KEY))
            {
                throw new Exception("Key must be of type " + typeof(KEY));
            }

            var castKey = (KEY)key;
            var reload = false;

            if (Values.ContainsKey(castKey)){
                var result = ReloadTest(castKey);
                if (result != null) return result;
                else reload = true;
            }

            lock (Values)
            {
                if (reload)
                {
                    TaskWrap<object> oldVal;
                    Values.TryRemove(castKey, out oldVal);

                    return Values.GetOrAdd(castKey, x =>
                        new TaskWrap<object>(ResolveMissingKey(castKey, (VALUE)oldVal.GetReady()))
                        ).Get();
                }
                else if (Values.ContainsKey(castKey))
                {
                    return Values[castKey].Get();
                }
                else
                {
                    return Values.GetOrAdd(castKey, x =>
                        new TaskWrap<object>(ResolveMissingKey(castKey))
                        ).Get();
                }
            }
        }

        public override void Invalidate(object key)
        {
            if (!(key is KEY)) return;
            lock (Values)
            {
                TaskWrap<object> oldval;
                Values.TryRemove((KEY)key, out oldval);
            }
        }

        private Task<object> ResolveMissingKey(object key)
        {
            return ResolveMissingKey(key, default(VALUE));
        }

        private Task<object> ResolveMissingKey(object key, VALUE oldVal)
        {
            var cts = new CancellationTokenSource(LazyLoadTimeout);
            return Task.Factory.StartNew<object>(() =>
            {
                return (object)LazyLoad((KEY)key, oldVal);
            }, cts.Token);
        }
        protected virtual VALUE LazyLoad(KEY key, VALUE oldVal)
        {
            return ModelActivator.NewInstance<VALUE>();
        }

        protected virtual VALUE LazyLoad(KEY key)
        {
            return LazyLoad(key, default(VALUE));
        }

        protected virtual bool RequiresReload(KEY key, VALUE value)
        {
            return false;
        }
    }

    //experimental class to get around a mono issue where task resources are not freed
    //when they are completed... only when they are deleted.
    public class TaskWrap<T>
    {
        private Task<T> Task { get; set; }
        private T Result;
        public bool Ready { get { return Result != null; } }

        public TaskWrap(Task<T> task) {
            Task = task;
        }

        public async Task<T> Get()
        {
            var t = Task;
            if (t != null)
            {
                var result = await t;
                Result = result;
                Task = null;
                return result;
            } else
            {
                return Result;
            }
        }

        public T GetReady()
        {
            return Result;
        }
    }
}
