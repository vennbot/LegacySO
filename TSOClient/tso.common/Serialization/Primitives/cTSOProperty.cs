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

namespace FSO.Common.Serialization.Primitives
{

    public class cTSOProperty : IoBufferSerializable, IoBufferDeserializable
    {
        public uint StructType;
        public List<cTSOPropertyField> StructFields;

        public void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutUInt32(0x89739A79);
            output.PutUInt32(StructType);
            output.PutUInt32((uint)StructFields.Count);

            foreach (var item in StructFields)
            {
                output.PutUInt32(item.StructFieldID);
                context.ModelSerializer.Serialize(output, item.Value, context, true);
            }
        }

        public void Deserialize(IoBuffer input, ISerializationContext context)
        {
            //Unknown
            input.GetUInt32();
            StructType = input.GetUInt32();

            StructFields = new List<cTSOPropertyField>();

            var numFields = input.GetUInt32();
            for(int i=0; i < numFields; i++){
                var fieldId = input.GetUInt32();
                var typeId = input.GetUInt32();
                var value = context.ModelSerializer.Deserialize(typeId, input, context);

                StructFields.Add(new cTSOPropertyField
                {
                    StructFieldID = fieldId,
                    Value = value
                });
            }
        }
    }

    public class cTSOPropertyField
    {
        public uint StructFieldID;
        public object Value;
    }
}
