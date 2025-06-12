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
using Mina.Core.Buffer;
using System;

namespace FSO.Common.Serialization.TypeSerializers
{
    public class cTSOValueUInt64 : ITypeSerializer
    {
        //0x69D3E3DB: cTSOValue<unsigned __int64>
        private readonly uint CLSID = 0x69D3E3DB;

        public bool CanDeserialize(uint clsid)
        {
            return clsid == CLSID;
        }

        public bool CanSerialize(Type type)
        {
            return type.IsAssignableFrom(typeof(ulong));
        }

        public object Deserialize(uint clsid, IoBuffer input, ISerializationContext serializer)
        {
            return input.GetUInt64();
        }

        public void Serialize(IoBuffer output, object value, ISerializationContext serializer)
        {
            output.PutUInt64((ulong)value);
        }

        public uint? GetClsid(object value)
        {
            return CLSID;
        }
    }
}
