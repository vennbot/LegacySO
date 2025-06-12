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
using System;
using System.Collections.Generic;
using Mina.Core.Buffer;
using System.Collections.Immutable;

namespace FSO.Common.Serialization.TypeSerializers
{
    public class cTSOValueBooleanMap : ITypeSerializer
    {
        private readonly uint CLSID = 0xC97757F5;

        public bool CanDeserialize(uint clsid)
        {
            return clsid == CLSID;
        }

        public bool CanSerialize(Type type)
        {
            return typeof(ImmutableDictionary<uint, bool>).IsAssignableFrom(type);
        }

        public object Deserialize(uint clsid, IoBuffer buffer, ISerializationContext serializer)
        {
            var result = new Dictionary<uint, bool>();
            var count = buffer.GetUInt32();
            for(int i=0; i < count; i++){
                var key = buffer.GetUInt32();
                result.Add(key, buffer.Get() > 0);
            }
            
            return ImmutableDictionary.ToImmutableDictionary(result);
        }

        public void Serialize(IoBuffer output, object value, ISerializationContext serializer)
        {
            var dict = (ImmutableDictionary<uint, bool>)value;
            output.PutUInt32((uint)dict.Count);
            foreach (var elem in dict)
            {
                output.PutUInt32(elem.Key);
                output.Put((byte)(elem.Value ? 1 : 0));
            }
        }

        public uint? GetClsid(object value)
        {
            return CLSID;
        }
    }
}
