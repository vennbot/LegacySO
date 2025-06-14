
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

namespace FSO.Common.Serialization
{
    public interface IModelSerializer
    {
        object Deserialize(uint clsid, IoBuffer input, ISerializationContext context);
        void Serialize(IoBuffer output, object obj, ISerializationContext context);
        void Serialize(IoBuffer output, object value, ISerializationContext context, bool clsIdPrefix);
        IoBuffer SerializeBuffer(object value, ISerializationContext context, bool clsIdPrefix);

        uint? GetClsid(object value);
        void AddTypeSerializer(ITypeSerializer serializer);
    }

    public interface ITypeSerializer
    {
        object Deserialize(uint clsid, IoBuffer input, ISerializationContext serializer);
        void Serialize(IoBuffer output, object value, ISerializationContext serializer);

        uint? GetClsid(object value);

        bool CanSerialize(Type type);
        bool CanDeserialize(uint clsid);
    }
}
