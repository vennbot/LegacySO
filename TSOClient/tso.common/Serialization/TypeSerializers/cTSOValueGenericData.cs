
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
using FSO.Common.Serialization.Primitives;
using Mina.Core.Buffer;
using System;
using System.Collections.Generic;

namespace FSO.Common.Serialization.TypeSerializers
{
    class cTSOValueGenericData : ITypeSerializer
    {
        private readonly uint CLSID = 0xA99AF3B7;

        public bool CanDeserialize(uint clsid)
        {
            return clsid == CLSID;
        }

        public bool CanSerialize(Type type)
        {
            return type.IsAssignableFrom(typeof(cTSOGenericData));
        }

        public object Deserialize(uint clsid, IoBuffer input, ISerializationContext serializer)
        {
            var result = new List<byte>();
            var iclsid = input.GetUInt32();
            var count = input.GetUInt32();
            for (int i = 0; i < count; i++)
            {
                result.Add(input.Get());
            }
            return new cTSOGenericData(result.ToArray());
        }

        public void Serialize(IoBuffer output, object value, ISerializationContext serializer)
        {
            var dat = (cTSOGenericData)value;
            output.PutUInt32(0x0A2C6585);
            output.PutUInt32((uint)dat.Data.Length);
            for (int i = 0; i < dat.Data.Length; i++)
            {
                output.Put(dat.Data[i]);
            }
        }

        public uint? GetClsid(object value)
        {
            return CLSID;
        }
    }
}
