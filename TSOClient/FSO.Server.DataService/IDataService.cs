
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
using FSO.Common.Serialization.Primitives;
using FSO.Files.Formats.tsodata;
using FSO.Server.DataService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSO.Common.DataService
{
    public interface IDataService
    {
        Task<T> Get<T>(object key);
        Task<T[]> GetMany<T>(object[] keys);

        Task<object> Get(Type type, object key);
        Task<object> Get(uint type, object key);
        Task<object> Get(MaskedStruct type, object key);

        void Invalidate<T>(object key);
        void Invalidate<T>(object key, T replacement);

        List<cTSOTopicUpdateMessage> SerializeUpdate(MaskedStruct mask, object value, uint id);
        List<cTSOTopicUpdateMessage> SerializeUpdate(StructField[] fields, object value, uint id);
        Task<cTSOTopicUpdateMessage> SerializePath(params uint[] dotPath);

        void ApplyUpdate(cTSOTopicUpdateMessage update, ISecurityContext context);

        StructField[] GetFieldsByName(Type type, params string[] fieldNames);
    }
}
