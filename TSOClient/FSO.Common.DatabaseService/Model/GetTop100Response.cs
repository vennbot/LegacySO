
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
using FSO.Common.Serialization;
using System.Collections.Generic;
using Mina.Core.Buffer;
using FSO.Common.DatabaseService.Framework;

namespace FSO.Common.DatabaseService.Model
{
    [DatabaseResponse(DBResponseType.GetTopResultSetByID)]
    public class GetTop100Response : IoBufferSerializable, IoBufferDeserializable
    {
        public List<Top100Entry> Items;

        public void Deserialize(IoBuffer input, ISerializationContext context)
        {
            var count = input.GetUInt32();
            Items = new List<Top100Entry>((int)count);

            for (var i=0; i < count; i++)
            {
                var item = new Top100Entry();
                item.Rank = input.Get();

                var hasValue = input.GetBool();
                if (hasValue){
                    item.TargetId = input.GetUInt32();
                    item.TargetName = input.GetPascalVLCString();
                }

                Items.Add(item);
            }
        }

        public void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutUInt32((uint)Items.Count);
            foreach (var item in Items)
            {
                output.Put(item.Rank);

                if(item.TargetId != null && item.TargetId.HasValue){
                    output.PutBool(true);
                    output.PutUInt32(item.TargetId.Value);
                    output.PutPascalVLCString(item.TargetName);
                }else{
                    output.PutBool(false);
                }
            }
        }
    }

    public class Top100Entry
    {
        public byte Rank;
        public uint? TargetId;
        public string TargetName;
    }
}
