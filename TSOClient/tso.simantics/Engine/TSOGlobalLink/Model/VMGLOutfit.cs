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
using FSO.Common.Serialization;
using Mina.Core.Buffer;

namespace FSO.SimAntics.Engine.TSOGlobalLink.Model
{
    public class VMGLOutfit : IoBufferSerializable, IoBufferDeserializable
    {
        public uint outfit_id { get; set; }
        public ulong asset_id { get; set; }
        public int sale_price { get; set; }
        public int purchase_price { get; set; }
        public VMGLOutfitOwner owner_type { get; set; }
        public uint owner_id { get; set; }
        public byte outfit_type { get; set; }
        public VMGLOutfitSource outfit_source { get; set; }

        public void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutUInt32(outfit_id);
            output.PutUInt64(asset_id);
            output.PutInt32(sale_price);
            output.PutInt32(purchase_price);
            output.PutEnum(owner_type);
            output.PutUInt32(owner_id);
            output.Put(outfit_type);
            output.PutEnum(outfit_source);
        }

        public void Deserialize(IoBuffer input, ISerializationContext context)
        {
            outfit_id = input.GetUInt32();
            asset_id = input.GetUInt64();
            sale_price = input.GetInt32();
            purchase_price = input.GetInt32();
            owner_type = input.GetEnum<VMGLOutfitOwner>();
            owner_id = input.GetUInt32();
            outfit_type = input.Get();
            outfit_source = input.GetEnum<VMGLOutfitSource>();
        }
    }


    public enum VMGLOutfitOwner : byte
    {
        AVATAR = 1,
        OBJECT = 2
    }

    public enum VMGLOutfitSource : byte
    {
        cas,
        rack
    }
}
