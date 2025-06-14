
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
using FSO.Server.Common;
using FSO.Server.Protocol.Voltron;
using FSO.Server.Protocol.Aries;

namespace tso.debug.network
{
    public class RawPacketReference
    {
        public Packet Packet;
        public int Sequence;
    }


    public static class PacketExtensions
    {
        public static string GetPacketName(this Packet packet)
        {
            switch (packet.Type)
            {
                case PacketType.VOLTRON:
                    return VoltronPacketTypeUtils.FromPacketCode((ushort)packet.SubType).ToString();
                case PacketType.ARIES:
                    return AriesPacketTypeUtils.FromPacketCode(packet.SubType).ToString();
            }
            return packet.Type.ToString() + " (" + packet.SubType + ")";
        }
    }
}
