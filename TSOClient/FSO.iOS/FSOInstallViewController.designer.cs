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
// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FSOiOS
{
    [Register ("FSOInstallViewController")]
    partial class FSOInstallViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton IpConfirm { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField IPEntry { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView StatusProgress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StatusText { get; set; }

        [Action ("IpConfirm_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void IpConfirm_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (IpConfirm != null) {
                IpConfirm.Dispose ();
                IpConfirm = null;
            }

            if (IPEntry != null) {
                IPEntry.Dispose ();
                IPEntry = null;
            }

            if (StatusProgress != null) {
                StatusProgress.Dispose ();
                StatusProgress = null;
            }

            if (StatusText != null) {
                StatusText.Dispose ();
                StatusText = null;
            }
        }
    }
}
