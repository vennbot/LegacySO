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
using MonoMac.AppKit;
using MonoMac.Foundation;
using MonoMac.CoreGraphics;
using System.Runtime.InteropServices;
 
namespace MonoTest
{
    class Program
    {
        static void Main (string [] args)
        {
            NSApplication.Init ();
 
            using (var p = new NSAutoreleasePool ()) {
                NSApplication.SharedApplication.Delegate = new AppDelegate();
                NSApplication.Main(args);
            }
        }
    }
 
    class AppDelegate : NSApplicationDelegate
    {
		Game1 game;
 
        public override void FinishedLaunching (MonoMac.Foundation.NSObject notification)
        {
            game = new Game1();
            game.Run ();
        }
 
        public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
        {
            return true;
        }
    }
}
