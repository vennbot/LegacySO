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

namespace Lidgren.Network
{
	/// <summary>
	/// Example class; not very good encryption
	/// </summary>
	public class NetXorEncryption : NetEncryption
	{
		private byte[] m_key;

		/// <summary>
		/// NetXorEncryption constructor
		/// </summary>
		public NetXorEncryption(NetPeer peer, byte[] key)
			: base(peer)
		{
			m_key = key;
		}

		public override void SetKey(byte[] data, int offset, int count)
		{
			m_key = new byte[count];
			Array.Copy(data, offset, m_key, 0, count);
		}

		/// <summary>
		/// NetXorEncryption constructor
		/// </summary>
		public NetXorEncryption(NetPeer peer, string key)
			: base(peer)
		{
			m_key = Encoding.UTF8.GetBytes(key);
		}

		/// <summary>
		/// Encrypt an outgoing message
		/// </summary>
		public override bool Encrypt(NetOutgoingMessage msg)
		{
			int numBytes = msg.LengthBytes;
			for (int i = 0; i < numBytes; i++)
			{
				int offset = i % m_key.Length;
				msg.m_data[i] = (byte)(msg.m_data[i] ^ m_key[offset]);
			}
			return true;
		}

		/// <summary>
		/// Decrypt an incoming message
		/// </summary>
		public override bool Decrypt(NetIncomingMessage msg)
		{
			int numBytes = msg.LengthBytes;
			for (int i = 0; i < numBytes; i++)
			{
				int offset = i % m_key.Length;
				msg.m_data[i] = (byte)(msg.m_data[i] ^ m_key[offset]);
			}
			return true;
		}
	}
}
