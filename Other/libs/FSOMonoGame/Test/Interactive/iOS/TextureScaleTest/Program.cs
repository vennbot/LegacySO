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
#region File Description
//-----------------------------------------------------------------------------
// Program.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;

#if IOS
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif


#endregion

namespace MonoTest
{
	[Register ("AppDelegate")]
	class Program : UIApplicationDelegate 
	{
		public override void FinishedLaunching (UIApplication app)
		{
			// Fun begins..
			using (Game1 game = new Game1())
            {
                game.Run();
            }
			
			//MediaLibrary lib = new MediaLibrary();
			//object result = lib.Playlists;
		}

		static void Main (string [] args)
		{
			UIApplication.Main (args,null,"AppDelegate");
		}
	}
}

