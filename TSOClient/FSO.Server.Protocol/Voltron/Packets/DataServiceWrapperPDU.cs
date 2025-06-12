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
using Mina.Core.Buffer;
using System.ComponentModel;
using FSO.Common.Serialization;

namespace FSO.Server.Protocol.Voltron.Packets
{
    public class DataServiceWrapperPDU : AbstractVoltronPacket
    {
        public uint SendingAvatarID { get; set; }
        public uint RequestTypeID { get; set; }
        
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public object Body { get; set; }

        public override VoltronPacketType GetPacketType()
        {
            return VoltronPacketType.DataServiceWrapperPDU;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutUInt32(SendingAvatarID);
            output.PutUInt32(RequestTypeID);

            if(Body != null){
                var bodyBytes = context.ModelSerializer.SerializeBuffer(Body, context, true);
                output.PutUInt32((uint)bodyBytes.Remaining);
                output.Put(bodyBytes);
            }

            //output.PutUInt32(RequestTypeID);
            //output.PutUInt32(0);
        }

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            this.SendingAvatarID = input.GetUInt32();
            this.RequestTypeID = input.GetUInt32();

            var bodySize = input.GetUInt32();
            var bodyBytes = new byte[bodySize];
            input.Get(bodyBytes, 0, (int)bodySize);
            this.Body = bodyBytes;
            var bodyBuffer = IoBuffer.Wrap(bodyBytes);
            var bodyType = bodyBuffer.GetUInt32();

            this.Body = context.ModelSerializer.Deserialize(bodyType, bodyBuffer, context);
        }
        
    }

    
}
