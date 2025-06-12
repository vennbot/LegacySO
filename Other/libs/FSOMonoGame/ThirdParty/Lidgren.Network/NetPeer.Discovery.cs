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
using System.Net;

namespace Lidgren.Network
{
	public partial class NetPeer
	{
		/// <summary>
		/// Emit a discovery signal to all hosts on your subnet
		/// </summary>
		public void DiscoverLocalPeers(int serverPort)
		{
			NetOutgoingMessage om = CreateMessage(0);
			om.m_messageType = NetMessageType.Discovery;
			m_unsentUnconnectedMessages.Enqueue(new NetTuple<IPEndPoint, NetOutgoingMessage>(new IPEndPoint(IPAddress.Broadcast, serverPort), om));
		}

		/// <summary>
		/// Emit a discovery signal to a single known host
		/// </summary>
		public bool DiscoverKnownPeer(string host, int serverPort)
		{
			IPAddress address = NetUtility.Resolve(host);
			if (address == null)
				return false;
			DiscoverKnownPeer(new IPEndPoint(address, serverPort));
			return true;
		}

		/// <summary>
		/// Emit a discovery signal to a single known host
		/// </summary>
		public void DiscoverKnownPeer(IPEndPoint endPoint)
		{
			NetOutgoingMessage om = CreateMessage(0);
			om.m_messageType = NetMessageType.Discovery;
			m_unsentUnconnectedMessages.Enqueue(new NetTuple<IPEndPoint, NetOutgoingMessage>(endPoint, om));
		}

		/// <summary>
		/// Send a discovery response message
		/// </summary>
		public void SendDiscoveryResponse(NetOutgoingMessage msg, IPEndPoint recipient)
		{
			if (recipient == null)
				throw new ArgumentNullException("recipient");

			if (msg == null)
				msg = CreateMessage(0);
			else if (msg.m_isSent)
				throw new NetException("Message has already been sent!");

			if (msg.LengthBytes >= m_configuration.MaximumTransmissionUnit)
				throw new NetException("Cannot send discovery message larger than MTU (currently " + m_configuration.MaximumTransmissionUnit + " bytes)");

			msg.m_messageType = NetMessageType.DiscoveryResponse;
			m_unsentUnconnectedMessages.Enqueue(new NetTuple<IPEndPoint, NetOutgoingMessage>(recipient, msg));
		}
	}
}
