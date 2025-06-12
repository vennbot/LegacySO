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

namespace Microsoft.Xna.Framework.Net
{
	internal class CommandSendData : ICommand
	{
		internal int gamerInternalIndex = -1;
		internal byte[] data;
		internal SendDataOptions options;
		internal int offset;
		internal int count;
		internal NetworkGamer gamer;
		internal LocalNetworkGamer sender;
		
		public CommandSendData (byte[] data, int offset, int count, SendDataOptions options, NetworkGamer gamer, LocalNetworkGamer sender)
		{
			if (gamer != null)
				gamerInternalIndex = gamer.Id;
			this.data = new byte[count];
			Array.Copy(data, offset, this.data, 0, count);
			this.offset = offset;
			this.count = count;
			this.options = options;
			this.gamer = gamer;
			this.sender = sender;
				
		}
		
		public CommandEventType Command {
			get { return CommandEventType.SendData; }
		}
	}
}

