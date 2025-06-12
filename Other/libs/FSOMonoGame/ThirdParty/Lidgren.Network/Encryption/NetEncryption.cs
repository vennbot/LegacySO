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
using System.Security.Cryptography;

namespace Lidgren.Network
{
	/// <summary>
	/// Interface for an encryption algorithm
	/// </summary>
	public abstract class NetEncryption
	{
		/// <summary>
		/// NetPeer
		/// </summary>
		protected NetPeer m_peer;

		/// <summary>
		/// Constructor
		/// </summary>
		public NetEncryption(NetPeer peer)
		{
			if (peer == null)
				throw new NetException("Peer must not be null");
			m_peer = peer;
		}

		public void SetKey(string str)
		{
			var bytes = System.Text.Encoding.ASCII.GetBytes(str);
			SetKey(bytes, 0, bytes.Length);
		}

		public abstract void SetKey(byte[] data, int offset, int count);

		/// <summary>
		/// Encrypt an outgoing message in place
		/// </summary>
		public abstract bool Encrypt(NetOutgoingMessage msg);

		/// <summary>
		/// Decrypt an incoming message in place
		/// </summary>
		public abstract bool Decrypt(NetIncomingMessage msg);
	}
}
