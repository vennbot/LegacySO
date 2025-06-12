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
	internal class CommandGamerStateChange : ICommand
	{
		
		GamerStates newState;
		GamerStates oldState;
		NetworkGamer gamer;
		
		public CommandGamerStateChange (NetworkGamer gamer)
		{
			this.gamer = gamer;
			this.newState = gamer.State;
			this.oldState = gamer.OldState;
		}
		
		public NetworkGamer Gamer 
		{
			get { return gamer; }
		}
		public GamerStates NewState
		{
			get { return newState; }
		}
		
		public GamerStates OldState
		{
			get { return oldState; }
		}
		
		public CommandEventType Command {
			get { return CommandEventType.GamerStateChange; }
		}		
	}
}

