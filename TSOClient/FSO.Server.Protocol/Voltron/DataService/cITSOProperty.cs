
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

namespace FSO.Server.Protocol.Voltron.DataService
{
    public class cITSOProperty : IoBufferSerializable
    {
        public uint StructType;
        public List<cITSOField> StructFields;
        
        /**cTSOValue<class cRZAutoRefCount<class cITSOProperty> > body:
* dword Body clsid (iid=896E3E90 or "GZIID_cITSOProperty"; clsid should be 0x89739A79 for cTSOProperty)
* dword Body
  * dword Struct type (e.g. 0x3B0430BF for AvatarAppearance)
  * dword Field count
  * Fields - for each field:
    * dword Field name (e.g. 0x1D530275 for AvatarAppearance_BodyOutfitID)
    * dword cTSOValue clsid
    * cTSOValue body**/

        public void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutUInt32(0x89739A79);
            output.PutUInt32(StructType);
            output.PutUInt32((uint)StructFields.Count);

            foreach(var item in StructFields){
                output.PutUInt32(item.ID);
                output.PutUInt32(item.Value.Type);
                output.PutSerializable(item.Value.Value, context);
            }
        }
    }

    public class cITSOField
    {
        public uint ID;
        public cTSOValue Value;
    }
}
