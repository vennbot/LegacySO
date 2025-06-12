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
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using TSOClient.Events;
using TSOClient.Network.Events;

namespace TSOClient.Network
{
    /// <summary>
    /// A packet that has been decrypted and processed, ready to read from.
    /// </summary>
    public class ProcessedPacket : PacketStream
    {
        public ushort DecryptedLength;

        public ProcessedPacket(byte ID, bool Encrypted, int Length, byte[] DataBuffer)
            : base(ID, Length, DataBuffer)
        {
            byte Opcode = (byte)this.ReadByte();
            this.m_Length = (ushort)this.ReadUShort();

            if (Encrypted)
            {
                this.DecryptedLength = (ushort)this.ReadUShort();

                if (this.DecryptedLength != this.m_Length)
                {
                    //Something's gone haywire, throw an error...
                    EventSink.RegisterEvent(new PacketError(EventCodes.PACKET_PROCESSING_ERROR));
                }
            }

            if(Encrypted)
                this.DecryptPacket(PlayerAccount.EncKey, new DESCryptoServiceProvider(), this.DecryptedLength);
        }
    }
}
