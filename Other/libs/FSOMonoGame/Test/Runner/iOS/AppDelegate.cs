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
using System.Linq;
using System.Net;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using MonoGame.Tests.Interface;

namespace MonoGame.Tests.iOS {
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate {

		private FileServer _fileServer;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// FIXME: Figure out how to pass and receive arguments
			//        in MonoTouch applications.  The Main method
			//        has an empty array and NSProcessInfo has
			//        values specific to Mono launching/debugging.
			MobileInterface.RunAsync (new string [0]);

			_fileServer = new FileServer ();
			_fileServer.Prefixes.Add("http://+:6599/");
			_fileServer.Start ();
			return true;
		}
	}
}
