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
using System.Collections.Generic;
using Mina.Core.Buffer;
using FSO.Common.Serialization;
using FSO.Common.DatabaseService.Framework;

namespace FSO.Common.DatabaseService.Model
{
    [DatabaseResponse(DBResponseType.Search)]
    [DatabaseResponse(DBResponseType.SearchExactMatch)]
    public class SearchResponse : IoBufferSerializable, IoBufferDeserializable
    {
        public string Query { get; set; }
        public SearchType Type { get; set; }
        public uint Unknown { get; set; }
        public List<SearchResponseItem> Items { get; set; }
        
        public void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutPascalVLCString(Query);
            output.PutUInt32((byte)Type);
            output.PutUInt32(Unknown);
            output.PutUInt32((uint)Items.Count);

            foreach(var item in Items){
                output.PutUInt32(item.EntityId);
                output.PutPascalVLCString(item.Name);
            }

            output.Skip(36);
        }

        public void Deserialize(IoBuffer input, ISerializationContext context)
        {
            Query = input.GetPascalVLCString();
            Type = (SearchType)((byte)input.GetUInt32());
            Unknown = input.GetUInt32();

            Items = new List<SearchResponseItem>();
            var count = input.GetUInt32();
            for(int i=0; i < count; i++)
            {
                var entityId = input.GetUInt32();
                var entityLabel = input.GetPascalVLCString();
                Items.Add(new SearchResponseItem { EntityId = entityId, Name = entityLabel });
            }

            input.Skip(36);
        }
    }

    public class SearchResponseItem
    {
        public uint EntityId { get; set; }
        public string Name { get; set; }
    }
}
