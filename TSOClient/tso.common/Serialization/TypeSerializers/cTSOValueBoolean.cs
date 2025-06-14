
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
using System;

namespace FSO.Common.Serialization.TypeSerializers
{
    public class cTSOValueBoolean : ITypeSerializer
    {
        private readonly uint CLSID = 0x696D1183;

        public bool CanDeserialize(uint clsid)
        {
            return clsid == CLSID;
        }

        public bool CanSerialize(Type type)
        {
            return type.IsAssignableFrom(typeof(bool));
        }

        public object Deserialize(uint clsid, IoBuffer input, ISerializationContext serializer)
        {
            var byteValue = input.Get();
            return byteValue == 0x01 ? true : false;
        }

        public void Serialize(IoBuffer output, object value, ISerializationContext serializer)
        {
            bool boolValue = (bool)value;
            output.Put(boolValue ? (byte)0x01 : (byte)0x00);
        }

        public uint? GetClsid(object value)
        {
            return CLSID;
        }
    }
}
