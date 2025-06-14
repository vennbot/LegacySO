
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

namespace FSO.Server.Protocol.Voltron.Packets
{
    public class SetIgnoreListResponsePDU : AbstractVoltronPacket
    {
        public uint StatusCode;
        public string ReasonText;
        public uint MaxNumberOfIgnored;

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            this.StatusCode = input.GetUInt32();
            this.ReasonText = input.GetPascalString();
            this.MaxNumberOfIgnored = input.GetUInt32();
        }

        public override VoltronPacketType GetPacketType()
        {
            return VoltronPacketType.SetIgnoreListResponsePDU;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            //var result = Allocate(8 + 4 + ReasonText.Length);
            output.PutUInt32(StatusCode);
            output.PutPascalString(this.ReasonText);
            output.PutUInt32(MaxNumberOfIgnored);
            //return result;
        }
    }
}
