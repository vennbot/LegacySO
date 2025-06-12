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
	/// How the library deals with resends and handling of late messages
	/// </summary>
	public enum NetDeliveryMethod : byte
	{
		//
		// Actually a publicly visible subset of NetMessageType
		//

		/// <summary>
		/// Indicates an error
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Unreliable, unordered delivery
		/// </summary>
		Unreliable = 1,

		/// <summary>
		/// Unreliable delivery, but automatically dropping late messages
		/// </summary>
		UnreliableSequenced = 2,

		/// <summary>
		/// Reliable delivery, but unordered
		/// </summary>
		ReliableUnordered = 34,

		/// <summary>
		/// Reliable delivery, except for late messages which are dropped
		/// </summary>
		ReliableSequenced = 35,

		/// <summary>
		/// Reliable, ordered delivery
		/// </summary>
		ReliableOrdered = 67,
	}
}
