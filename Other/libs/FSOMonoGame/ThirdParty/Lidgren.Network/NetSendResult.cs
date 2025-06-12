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
	/// <summary>
	/// Result of a SendMessage call
	/// </summary>
	public enum NetSendResult
	{
		/// <summary>
		/// Message failed to enqueue because there is no connection
		/// </summary>
		FailedNotConnected = 0,

		/// <summary>
		/// Message was immediately sent
		/// </summary>
		Sent = 1,

		/// <summary>
		/// Message was queued for delivery
		/// </summary>
		Queued = 2,

		/// <summary>
		/// Message was dropped immediately since too many message were queued
		/// </summary>
		Dropped = 3
	}
}
