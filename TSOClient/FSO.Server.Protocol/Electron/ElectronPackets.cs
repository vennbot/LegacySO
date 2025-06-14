
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
using FSO.Server.Protocol.Electron.Packets;
using System;
using System.Collections.Generic;

namespace FSO.Server.Protocol.Electron
{
    public class ElectronPackets
    {
        public static Dictionary<ushort, Type> ELECTRON_PACKET_BY_TYPEID;
        public static Type[] ELECTRON_PACKETS = new Type[] {
            typeof(CreateASimResponse),
            typeof(PurchaseLotRequest),
            typeof(PurchaseLotResponse),
            typeof(InstantMessage),
            typeof(FindLotRequest),
            typeof(FindLotResponse),
            typeof(FSOVMTickBroadcast),
            typeof(FSOVMDirectToClient),
            typeof(FSOVMCommand),
            typeof(FindAvatarRequest),
            typeof(FindAvatarResponse),
            typeof(ChangeRoommateRequest),
            typeof(KeepAlive),
            typeof(ChangeRoommateResponse),
            typeof(ModerationRequest),
            typeof(FSOVMProtocolMessage),
            typeof(AvatarRetireRequest),
            typeof(MailRequest),
            typeof(MailResponse),
            typeof(NhoodRequest),
            typeof(NhoodResponse),
            typeof(NhoodCandidateList),
            typeof(BulletinRequest),
            typeof(BulletinResponse),
            typeof(GlobalTuningUpdate)
        };

        static ElectronPackets()
        {
            ELECTRON_PACKET_BY_TYPEID = new Dictionary<ushort, Type>();
            foreach (Type packetType in ELECTRON_PACKETS)
            {
                IElectronPacket packet = (IElectronPacket)Activator.CreateInstance(packetType);
                ELECTRON_PACKET_BY_TYPEID.Add(packet.GetPacketType().GetPacketCode(), packetType);
            }
        }

        public static Type GetByPacketCode(ushort code)
        {
            if (ELECTRON_PACKET_BY_TYPEID.ContainsKey(code))
            {
                return ELECTRON_PACKET_BY_TYPEID[code];
            }
            else
            {
                return null;
            }
        }
    }
}
