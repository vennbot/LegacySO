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

namespace FSO.Server.Protocol.Gluon.Packets
{
    public class TuningChanged : AbstractGluonCallPacket
    {
        public bool UpdateInstantly;

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            base.Deserialize(input, context);
            UpdateInstantly = input.GetBool();
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            base.Serialize(output, context);
            output.PutBool(UpdateInstantly);
        }

        public override GluonPacketType GetPacketType()
        {
            return GluonPacketType.TuningChanged;
        }
    }
}
