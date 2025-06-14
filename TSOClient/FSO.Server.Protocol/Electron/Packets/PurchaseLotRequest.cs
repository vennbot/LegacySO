
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
    public class PurchaseLotRequest : AbstractElectronPacket
    {
        public ushort LotLocation_X;
        public ushort LotLocation_Y;
        public string Name;
        public bool StartFresh;
        public bool MayorMode;

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            LotLocation_X = input.GetUInt16();
            LotLocation_Y = input.GetUInt16();
            Name = input.GetPascalString();
            StartFresh = input.GetBool();
            MayorMode = input.GetBool();
        }

        public override ElectronPacketType GetPacketType()
        {
            return ElectronPacketType.PurchaseLotRequest;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutUInt16(LotLocation_X);
            output.PutUInt16(LotLocation_Y);
            output.PutPascalString(Name);
            output.PutBool(StartFresh);
            output.PutBool(MayorMode);
        }
    }
}
