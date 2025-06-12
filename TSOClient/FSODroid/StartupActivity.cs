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
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Content.PM;
using System.IO;
using FSODroid.Resources.Layout;
using System.Threading;

namespace FSODroid
{
    [Activity(Label = "FreeSO"
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.SensorLandscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
        , MainLauncher = true)]
    public class StartupActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            new Thread(() =>
            {
                Thread.Sleep(100);
                RunOnUiThread(() =>
                {
                    Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
                    Window.AddFlags(WindowManagerFlags.Fullscreen); //to show
                    if (File.Exists(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "The Sims Online/TSOClient/tuning.dat")))
                    {
                        var activity2 = new Intent(this, typeof(FSOActivity));
                        StartActivity(activity2);
                    }
                    else
                    {
                        var activity2 = new Intent(this, typeof(InstallerActivity));
                        StartActivity(activity2);
                    }
                });
            }).Start();
        }
    }
}
