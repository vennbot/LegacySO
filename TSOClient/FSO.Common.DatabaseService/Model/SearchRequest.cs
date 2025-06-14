
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
using Mina.Core.Buffer;
using FSO.Common.Serialization;
using FSO.Common.DatabaseService.Framework;

namespace FSO.Common.DatabaseService.Model
{
    [DatabaseRequest(DBRequestType.Search)]
    [DatabaseRequest(DBRequestType.SearchExactMatch)]
    public class SearchRequest : IoBufferSerializable, IoBufferDeserializable
    {
        public string Query { get; set; }
        public SearchType Type { get; set; }
        
        public void Deserialize(IoBuffer input, ISerializationContext context)
        {
            this.Query = input.GetPascalVLCString();
            this.Type = (SearchType)input.GetUInt32();
        }

        public void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutPascalVLCString(this.Query);
            output.PutUInt32((uint)Type);
        }
    }

    public enum SearchType
    {
        SIMS = 0x01,
        LOTS = 0x02,
        NHOOD = 0x03
    }
}
