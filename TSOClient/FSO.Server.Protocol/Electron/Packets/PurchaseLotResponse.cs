
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

namespace FSO.Server.Protocol.Electron.Packets
{
    public class PurchaseLotResponse : AbstractElectronPacket
    {
        public PurchaseLotStatus Status { get; set; }
        public PurchaseLotFailureReason Reason { get; set; } = PurchaseLotFailureReason.NONE;
        public uint NewLotId { get; set; }
        public int NewFunds { get; set; }

        public override ElectronPacketType GetPacketType()
        {
            return ElectronPacketType.PurchaseLotResponse;
        }

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            Status = input.GetEnum<PurchaseLotStatus>();
            Reason = input.GetEnum<PurchaseLotFailureReason>();
            NewLotId = input.GetUInt32();
            NewFunds = input.GetInt32();
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutEnum<PurchaseLotStatus>(Status);
            output.PutEnum<PurchaseLotFailureReason>(Reason);
            output.PutUInt32(NewLotId);
            output.PutInt32(NewFunds);
        }
    }

    public enum PurchaseLotStatus
    {
        SUCCESS = 0x01,
        FAILED = 0x02
    }

    public enum PurchaseLotFailureReason
    {
        NONE = 0x00,
        NAME_TAKEN = 0x01,
        NAME_VALIDATION_ERROR = 0x02,
        INSUFFICIENT_FUNDS = 0x03,
        LOT_TAKEN = 0x04,
        LOT_NOT_PURCHASABLE = 0x05,
        IN_LOT_CANT_EVICT = 0x06,
        NOT_OFFLINE_FOR_MOVE = 0x07,
        LOCATION_TAKEN = 0x08,
        NHOOD_RESERVED = 0x09,

        TH_NOT_MAYOR = 0x80,
        TH_INCORRECT_NHOOD = 0x81,


        UNKNOWN = 0xFF
    }
}
