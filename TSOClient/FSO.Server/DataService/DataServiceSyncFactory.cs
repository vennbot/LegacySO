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
using System.Linq;
using FSO.Server.Framework.Aries;
using FSO.Common.DataService;
using FSO.Files.Formats.tsodata;
using System.Reflection;
using FSO.Common.DataService.Framework.Attributes;
using FSO.Server.Protocol.Voltron.Packets;

namespace FSO.Server.DataService
{
    public class DataServiceSyncFactory : IDataServiceSyncFactory
    {
        private IDataService DataService;

        public DataServiceSyncFactory(IDataService ds)
        {
            this.DataService = ds;
        }

        public IDataServiceSync<T> Get<T>(params string[] fields)
        {
            return new DataServiceSync<T>(DataService, fields);
        }
    }

    public class DataServiceSync<T> : IDataServiceSync<T>
    {
        private IDataService DataService;
        private StructField[] Fields;
        private PropertyInfo KeyField;

        public DataServiceSync(IDataService ds, string[] fields)
        {
            this.DataService = ds;
            this.Fields = ds.GetFieldsByName(typeof(T), fields);
            this.KeyField = typeof(T).GetProperties().First(x => x.GetCustomAttribute<Key>() != null);
        }

        public void Sync(IAriesSession target, T item)
        {
            var asObject = (object)item;
            var updates = DataService.SerializeUpdate(Fields, asObject, (uint)KeyField.GetValue(asObject));

            if (updates.Count == 0) { return; }
            var packets = new DataServiceWrapperPDU[updates.Count];

            for(int i=0; i < updates.Count; i++)
            {
                var update = updates[i];
                packets[i] = new DataServiceWrapperPDU() {
                    Body = update,
                    RequestTypeID = 0,
                    SendingAvatarID = 0
                };
            }

            target.Write(packets);
        }
    }
}
