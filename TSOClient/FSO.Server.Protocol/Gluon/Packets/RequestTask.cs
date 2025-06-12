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
using FSO.Common.Serialization;
using Mina.Core.Buffer;

namespace FSO.Server.Protocol.Gluon.Packets
{
    public class RequestTask : AbstractGluonCallPacket
    {
        public string TaskType { get; set; }
        public string ParameterJson { get; set; }
        public int ShardId { get; set; }

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            base.Deserialize(input, context);
            TaskType = input.GetPascalString();
            ShardId = input.GetInt32();
            ParameterJson = input.GetString(Encoding.UTF8);
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            base.Serialize(output, context);
            output.PutPascalString(TaskType);
            output.PutInt32(ShardId);
            output.PutString(ParameterJson, Encoding.UTF8);
        }

        public override GluonPacketType GetPacketType()
        {
            return GluonPacketType.RequestTask;
        }
    }
}
