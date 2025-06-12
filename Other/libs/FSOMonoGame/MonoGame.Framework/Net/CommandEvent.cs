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
	internal class CommandEvent
	{
		CommandEventType command;
		object commandObject;
		
		public CommandEvent (CommandEventType command, object commandObject)
		{
			this.command = command;
			this.commandObject = commandObject;
		}
		
		public CommandEvent (ICommand command)
		{
			this.command = command.Command;
			this.commandObject = command;
		}		
		
		public CommandEventType Command
		{
			get { return command; }
			
		}
		
		public object CommandObject
		{
			get { return commandObject; }
		}
	}
}

