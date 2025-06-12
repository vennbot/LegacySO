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
using FSO.Common.Serialization;

namespace FSO.Server.Protocol.Voltron.Packets
{
    public class SetIgnoreListPDU : AbstractVoltronPacket
    {
        public List<uint> PlayerIds;

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            var length = input.GetUInt16();
            PlayerIds = new List<uint>();

            for(var i=0; i < length; i++)
            {
                PlayerIds.Add(input.GetUInt32());
            }
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            var len = 0;
            if(PlayerIds != null)
            {
                len = PlayerIds.Count;
            }

            output.PutUInt16((ushort)len);

            for(int i=0; i < len; i++)
            {
                output.PutUInt32(PlayerIds[i]);
            }
        }

        public override VoltronPacketType GetPacketType()
        {
            return VoltronPacketType.SetIgnoreListPDU;
        }
    }
}
