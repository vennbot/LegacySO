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
using System.Text;
using Mina.Core.Buffer;
using FSO.Common.Serialization;

namespace FSO.Server.Protocol.Aries.Packets
{
    public class RequestClientSessionResponse : IAriesPacket
    {
        public string User { get; set; }
        public string AriesVersion { get; set; }
        public string Email { get; set; }
        public string Authserv { get; set; }
        public ushort Product { get; set; }
        public byte Unknown { get; set; } = 39;
        public string ServiceIdent { get; set; }
        public ushort Unknown2 { get; set; } = 4; //1 if re-establishing
        public string Password { get; set; }

        public void Deserialize(IoBuffer input, ISerializationContext context)
        {
            this.User = input.GetString(112, Encoding.ASCII);
            this.AriesVersion = input.GetString(80, Encoding.ASCII);
            this.Email = input.GetString(40, Encoding.ASCII);
            this.Authserv = input.GetString(84, Encoding.ASCII);
            this.Product = input.GetUInt16();
            this.Unknown = input.Get();
            this.ServiceIdent = input.GetString(3, Encoding.ASCII);
            this.Unknown2 = input.GetUInt16();
            this.Password = input.GetString(32, Encoding.ASCII);
        }

        public AriesPacketType GetPacketType()
        {
            return AriesPacketType.RequestClientSessionResponse;
        }

        public void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutString(this.User, 112, Encoding.ASCII);
            output.PutString(this.AriesVersion, 80, Encoding.ASCII);
            output.PutString(this.Email, 40, Encoding.ASCII);
            output.PutString(this.Authserv, 84, Encoding.ASCII);
            output.PutUInt16(this.Product);
            output.Put(this.Unknown);
            output.PutString(this.ServiceIdent, 3, Encoding.ASCII);
            output.PutUInt16(this.Unknown2);
            output.PutString(this.Password, 32, Encoding.ASCII);
        }
    }
}
