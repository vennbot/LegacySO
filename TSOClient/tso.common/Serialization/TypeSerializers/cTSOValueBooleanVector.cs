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
    public class cTSOValueBooleanVector : ITypeSerializer
    {
        private readonly uint CLSID = 0x89738492;

        public bool CanDeserialize(uint clsid)
        {
            return clsid == CLSID;
        }

        public bool CanSerialize(Type type)
        {
            return typeof(IList<bool>).IsAssignableFrom(type);
        }

        public object Deserialize(uint clsid, IoBuffer buffer, ISerializationContext serializer)
        {
            var result = new List<bool>();
            var count = buffer.GetUInt32();
            for(int i=0; i < count; i++){
                result.Add(buffer.Get() > 0);
            }
            return ImmutableList.ToImmutableList(result);
        }

        public void Serialize(IoBuffer output, object value, ISerializationContext serializer)
        {
            IList<bool> list = (IList<bool>)value;
            output.PutUInt32((uint)list.Count);
            byte last = 0;
            int pos = 0;
            for (int i = 0; i < list.Count; i++)
            {
                output.Put((byte)(list[i]?1:0));
                //output.Put((byte)(1));
                /*last |= (byte)((list[i] ? 1 : 0) << pos++);

                if (pos >= 8)
                {
                    output.Put(last);
                    pos = 0;
                    last = 0;
                }*/
            }
        }

        public uint? GetClsid(object value)
        {
            return CLSID;
        }
    }
}
