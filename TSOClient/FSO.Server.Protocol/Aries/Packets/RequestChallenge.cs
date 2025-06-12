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
using FSO.Common.Serialization;
using Mina.Core.Buffer;

namespace FSO.Server.Protocol.Aries.Packets
{
    public class RequestChallenge : IAriesPacket
    {
        public string CallSign;
        public string PublicHost;
        public string InternalHost;

        public void Deserialize(IoBuffer input, ISerializationContext context)
        {
            CallSign = input.GetPascalString();
            PublicHost = input.GetPascalString();
            InternalHost = input.GetPascalString();
        }

        public AriesPacketType GetPacketType()
        {
            return AriesPacketType.RequestChallenge;
        }

        public void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutPascalString(CallSign);
            output.PutPascalString(PublicHost);
            output.PutPascalString(InternalHost);
        }
    }
}
