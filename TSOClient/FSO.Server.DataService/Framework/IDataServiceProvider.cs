
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
    public interface IDataServiceProvider
    {
        void Init();

        Task<object> Get(object key);
        void Invalidate(object key);
        void Invalidate(object key, object replacement);
        void DemandMutation(object entity, MutationType type, string path, object value, ISecurityContext context);
        void PersistMutation(object entity, MutationType type, string path, object value);

        Type GetKeyType();
        Type GetValueType();
    }

    public enum MutationType
    {
        SET_FIELD_VALUE,
        ARRAY_SET_ITEM,
        ARRAY_REMOVE_ITEM
    }
}
