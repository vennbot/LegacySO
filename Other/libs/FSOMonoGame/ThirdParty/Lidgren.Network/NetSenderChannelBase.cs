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

namespace Lidgren.Network
{
	internal abstract class NetSenderChannelBase
	{
		// access this directly to queue things in this channel
		internal NetQueue<NetOutgoingMessage> m_queuedSends;

		internal abstract int WindowSize { get; }

		internal abstract int GetAllowedSends();

		internal abstract NetSendResult Enqueue(NetOutgoingMessage message);
		internal abstract void SendQueuedMessages(float now);
		internal abstract void Reset();
		internal abstract void ReceiveAcknowledge(float now, int sequenceNumber);
	}
}
