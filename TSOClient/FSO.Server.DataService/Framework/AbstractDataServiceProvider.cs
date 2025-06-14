
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
using FSO.Common.Security;
using System;
using System.Threading.Tasks;

namespace FSO.Common.DataService.Framework
{
    public abstract class AbstractDataServiceProvider<KEY, VALUE> : IDataServiceProvider where VALUE : IModel
    {
        public virtual void DemandMutation(object entity, MutationType type, string path, object value, ISecurityContext context)
        {
        }

        public virtual void PersistMutation(object entity, MutationType type, string path, object value)
        {
        }

        public virtual void Init()
        {
        }

        public abstract Task<object> Get(object key);
        
        public Type GetKeyType()
        {
            return typeof(KEY);
        }

        public Type GetValueType()
        {
            return typeof(VALUE);
        }

        public virtual void Invalidate(object key)
        {
        }

        public virtual void Invalidate(object key, object replacement)
        {

        }
    }

}
