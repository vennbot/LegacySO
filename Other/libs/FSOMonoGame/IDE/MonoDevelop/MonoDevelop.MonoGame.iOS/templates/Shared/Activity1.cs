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

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using Android.OS;

using Microsoft.Xna.Framework;

namespace ${Namespace}
{
	[Activity (Label = "${ProjectName}", 
	           MainLauncher = true,
	           Icon = "@drawable/icon",
	           Theme = "@style/Theme.Splash",
                AlwaysRetainTaskState=true,
	           LaunchMode=LaunchMode.SingleInstance,
	           ConfigurationChanges = ConfigChanges.Orientation | 
	                                  ConfigChanges.KeyboardHidden | 
	                                  ConfigChanges.Keyboard)]
	public class Activity1 : AndroidGameActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create our OpenGL view, and display it
			Game1.Activity = this;
			var g = new Game1();
			SetContentView (g.Window);
			g.Run();
		}
		
	}
}

