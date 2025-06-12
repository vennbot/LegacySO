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

namespace FSO.Server.Protocol.Gluon.Packets
{
    public class AdvertiseCapacity : AbstractGluonPacket
    {
        public short MaxLots;
        public short CurrentLots;
        public byte CpuPercentAvg;
        public long RamUsed;
        public long RamAvaliable;

        public override GluonPacketType GetPacketType()
        {
            return GluonPacketType.AdvertiseCapacity;
        }

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            MaxLots = input.GetInt16();
            CurrentLots = input.GetInt16();
            CpuPercentAvg = input.Get();
            RamUsed = input.GetInt64();
            RamAvaliable = input.GetInt64();
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutInt16(MaxLots);
            output.PutInt16(CurrentLots);
            output.Put(CpuPercentAvg);
            output.PutInt64(RamUsed);
            output.PutInt64(RamAvaliable);
        }
    }
}
