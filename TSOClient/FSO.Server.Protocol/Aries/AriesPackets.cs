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
using FSO.Server.Protocol.Aries.Packets;
using System;
using System.Collections.Generic;

namespace FSO.Server.Protocol.Aries
{
    public class AriesPackets
    {
        public static Dictionary<uint, Type> ARIES_PACKET_BY_TYPEID;
        public static Type[] ARIES_PACKETS = new Type[] {
            typeof(RequestClientSession),
            typeof(RequestClientSessionResponse),
            typeof(RequestChallenge),
            typeof(RequestChallengeResponse),
            typeof(AnswerChallenge),
            typeof(AnswerAccepted)
        };

        static AriesPackets()
        {
            ARIES_PACKET_BY_TYPEID = new Dictionary<uint, Type>();
            foreach (Type packetType in ARIES_PACKETS)
            {
                IAriesPacket packet = (IAriesPacket)Activator.CreateInstance(packetType);
                ARIES_PACKET_BY_TYPEID.Add(packet.GetPacketType().GetPacketCode(), packetType);
            }
        }

        public static Type GetByPacketCode(uint code)
        {
            if (ARIES_PACKET_BY_TYPEID.ContainsKey(code))
            {
                return ARIES_PACKET_BY_TYPEID[code];
            }
            else
            {
                return null;
            }
        }
    }
}
