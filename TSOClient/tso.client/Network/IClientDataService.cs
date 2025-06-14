
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
using FSO.Client.Network;
using FSO.Server.DataService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSO.Common.DataService
{
    public interface IClientDataService : IDataService
    {
        Task<object> Request(MaskedStruct mask, uint id);
        void Sync(object item, string[] fields);
        void AddToArray(object item, string fieldPath, object value);
        void RemoveFromArray(object item, string fieldPath, object value);
        void SetArrayItem(object item, string fieldPath, uint index, object value);
        
        /// <summary>
        /// Enriches a list of data items with the associated data service object.
        /// For example, when searching avatars you have a list of avatar ids. This 
        /// utility cna be used to find all the Avatar objects that go with that result
        /// set
        /// </summary>
        List<OUTPUT> EnrichList<OUTPUT, INPUT, DSENTITY>(List<INPUT> input, Func<INPUT, uint> idFunction, Func<INPUT, DSENTITY, OUTPUT> outputConverter);
        List<OUTPUT> EnrichList<OUTPUT, INPUT, DSENTITY>(List<INPUT> input, Func<INPUT, uint?> idFunction, Func<INPUT, DSENTITY, OUTPUT> outputConverter);

        ITopicSubscription CreateTopicSubscription();

    }
}
