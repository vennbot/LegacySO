
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
using Mina.Filter.Codec;
using Mina.Core.Session;
using Mina.Core.Buffer;
using FSO.SimAntics.NetPlay.Model;

namespace FSO.Client.Network.Sandbox
{
    public class FSOSandboxProtocolEncoder : IProtocolEncoder
    {
        public void Dispose(IoSession session)
        {
        }

        public void Encode(IoSession session, object message, IProtocolEncoderOutput output)
        {
            if (message is object[])
            {
                foreach (var m in (object[])message) Encode(session, m, output);
            }
            else if (message is VMNetMessage)
            {
                var nmsg = (VMNetMessage)message;

                var payload = IoBuffer.Allocate(128);
                payload.Order = ByteOrder.LittleEndian;
                payload.AutoExpand = true;

                payload.PutInt32(0); //packet type
                payload.PutInt32(nmsg.Data.Length + 1);
                payload.Put((byte)nmsg.Type);
                foreach (var b in nmsg.Data) payload.Put(b);
                payload.Flip();

                output.Write(payload);
            }
        }
    }
}
