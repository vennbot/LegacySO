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

namespace FSO.Server.Protocol.Electron.Packets
{
    public class FSOVMDirectToClient : AbstractElectronPacket
    {
        public byte[] Data;

        public override void Deserialize(IoBuffer input, ISerializationContext context)
        {
            var dataLen = input.GetInt32(); //TODO: limits? 4MB is probably reasonable.
            Data = new byte[dataLen];
            input.Get(Data, 0, dataLen);
        }

        public override ElectronPacketType GetPacketType()
        {
            return ElectronPacketType.FSOVMDirectToClient;
        }

        public override void Serialize(IoBuffer output, ISerializationContext context)
        {
            output.PutInt32(Data.Length);
            output.Put(Data, 0, Data.Length);
        }
    }
}
