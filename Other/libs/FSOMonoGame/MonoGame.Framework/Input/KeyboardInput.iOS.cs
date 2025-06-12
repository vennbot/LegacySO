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
using System.Threading.Tasks;
using UIKit;

namespace Microsoft.Xna.Framework.Input
{
    public static partial class KeyboardInput
    {
        private static TaskCompletionSource<string> tcs;
        private static UIAlertView alert;

        private static Task<string> PlatformShow(string title, string description, string defaultText, bool usePasswordMode)
        {
            tcs = new TaskCompletionSource<string>();

            UIApplication.SharedApplication.InvokeOnMainThread(delegate
            {
                alert = new UIAlertView();
                alert.Title = title;
                alert.Message = description;
                alert.AlertViewStyle = usePasswordMode ? UIAlertViewStyle.SecureTextInput : UIAlertViewStyle.PlainTextInput;
                alert.AddButton("Cancel");
                alert.AddButton("Ok");
                UITextField alertTextField = alert.GetTextField(0);
                alertTextField.KeyboardType = UIKeyboardType.ASCIICapable;
                alertTextField.AutocorrectionType = UITextAutocorrectionType.No;
                alertTextField.AutocapitalizationType = UITextAutocapitalizationType.Sentences;
                alertTextField.Text = defaultText;
                alert.Dismissed += (sender, e) =>
                {
                    if (!tcs.Task.IsCompleted)
                        tcs.SetResult(e.ButtonIndex == 0 ? null : alert.GetTextField(0).Text);
                };

                // UIAlertView's textfield does not show keyboard in iOS8
                // http://stackoverflow.com/questions/25563108/uialertviews-textfield-does-not-show-keyboard-in-ios8
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                    alert.Presented += (sender, args) => alertTextField.SelectAll(alert);

                alert.Show();
            });

            return tcs.Task;
        }

        private static void PlatformCancel(string result)
        {
            if (!tcs.Task.IsCompleted)
                tcs.SetResult(result);

            UIApplication.SharedApplication.InvokeOnMainThread(delegate
            {
                alert.DismissWithClickedButtonIndex(0, true);
            });
        }
    }
}
