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
using FSO.Common.Serialization;
using FSO.Common.DatabaseService.Framework;

namespace FSO.Common.DatabaseService.Model
{
    [DatabaseRequest(DBRequestType.LoadAvatarByID)]
    public class LoadAvatarByIDRequest : IoBufferSerializable, IoBufferDeserializable
    {
        public uint AvatarId;
        public uint Unknown1 = 0;
        public uint Unknown2 = 0x69f4d5e8;

        public void Deserialize(IoBuffer input, ISerializationContext context)
        {
            this.AvatarId = input.GetUInt32();
            this.Unknown1 = input.GetUInt32();

            //Reserved - 32 bytes of uninitialized memory (just like Heartbleed); equal to "BA AD F0 0D BA AD F0 0D ..." if you are running the game in a debugger
            input.Skip(32);

            this.Unknown2 = input.GetUInt32();
        }

        public void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutUInt32(AvatarId);
            output.PutUInt32(Unknown1);
            output.Skip(32);
            output.PutUInt32(Unknown2);
        }
    }
}
