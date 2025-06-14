
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
using System.Collections.Generic;
using System.Collections.Immutable;

namespace FSO.Common.Serialization.TypeSerializers
{
    class cTSOValueUInt32Vector : ITypeSerializer
    {
        //0x89738496: cTSOValueVector<unsigned long>
        private readonly uint CLSID = 0x89738496;

        public bool CanDeserialize(uint clsid)
        {
            return clsid == CLSID;
        }

        public bool CanSerialize(Type type)
        {
            return typeof(IList<uint>).IsAssignableFrom(type);
        }

        public object Deserialize(uint clsid, IoBuffer input, ISerializationContext serializer)
        {
            var result = new List<uint>();
            var count = input.GetUInt32();
            for (int i = 0; i < count; i++)
            {
                result.Add((uint)input.GetUInt32());
            }
            return ImmutableList.ToImmutableList(result);
        }

        public void Serialize(IoBuffer output, object value, ISerializationContext serializer)
        {
            IList<uint> list = (IList<uint>)value;
            output.PutUInt32((uint)list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                output.PutUInt32((uint)list[i]);
            }
        }

        public uint? GetClsid(object value)
        {
            return CLSID;
        }
    }
}
