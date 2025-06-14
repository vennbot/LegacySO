
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
using FSO.Server.Protocol.Gluon.Packets;
using System;
using System.Collections.Generic;

namespace FSO.Server.Protocol.Gluon
{
    public class GluonPackets
    {
        public static Dictionary<ushort, Type> GLUON_PACKET_BY_TYPEID;
        public static Type[] ELECTRON_PACKETS = new Type[] {
            typeof(AdvertiseCapacity),
            typeof(TransferClaim),
            typeof(TransferClaimResponse),
            typeof(RequestLotClientTermination),
            typeof(ShardShutdownRequest),
            typeof(ShardShutdownCompleteResponse),
            typeof(HealthPing),
            typeof(HealthPingResponse),
            typeof(RequestTask),
            typeof(RequestTaskResponse),
            typeof(NotifyLotRoommateChange),
            typeof(MatchmakerNotify),
            typeof(CityNotify),
            typeof(TuningChanged),
            typeof(SendCityMail)
        };

        static GluonPackets()
        {
            GLUON_PACKET_BY_TYPEID = new Dictionary<ushort, Type>();
            foreach (Type packetType in ELECTRON_PACKETS)
            {
                IGluonPacket packet = (IGluonPacket)Activator.CreateInstance(packetType);
                GLUON_PACKET_BY_TYPEID.Add(packet.GetPacketType().GetPacketCode(), packetType);
            }
        }

        public static Type GetByPacketCode(ushort code)
        {
            if (GLUON_PACKET_BY_TYPEID.ContainsKey(code))
            {
                return GLUON_PACKET_BY_TYPEID[code];
            }
            else
            {
                return null;
            }
        }
    }
}
