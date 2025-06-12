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
#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#if MONOMAC
using MonoMac.AppKit;
using MonoMac.Foundation;
#elif __IOS__ || __TVOS__
using Foundation;
using UIKit;
#endif
#endregion

namespace BlankApp.tvOS
{
    #if __IOS__ || __TVOS__
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    #else
    static class Program
    #endif
    {
		private static Game1 game;

		internal static void RunGame() 
		{
			game = new Game1();
			game.Run();
			#if !__IOS__  && !__TVOS__
			game.Dispose ();
			#endif
		}
            
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        #if !MONOMAC && !__IOS__  && !__TVOS__
        [STAThread]
        #endif
        static void Main(string[] args)
        {
            #if MONOMAC
            NSApplication.Init ();

            using (var p = new NSAutoreleasePool ()) {
                NSApplication.SharedApplication.Delegate = new AppDelegate();
                NSApplication.Main(args);
            }
            #elif __IOS__ || __TVOS__
            UIApplication.Main(args, null, "AppDelegate");
            #else
            RunGame();
            #endif
        }

        #if __IOS__ || __TVOS__
        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
        #endif
    }

    #if MONOMAC
    class AppDelegate : NSApplicationDelegate
    {
        public override void FinishedLaunching (MonoMac.Foundation.NSObject notification)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (object sender, ResolveEventArgs a) =>  {
                if (a.Name.StartsWith("MonoMac")) {
                    return typeof(MonoMac.AppKit.AppKitFramework).Assembly;
                }
                return null;
            };
            Program.RunGame();
        }

        public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
        {
            return true;
        }
    }  
    #endif
}
