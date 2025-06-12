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
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;

namespace Microsoft.Xna.Framework.Input
{
    public static partial class MessageBox
    {
        private static TaskCompletionSource<int?> tcs;
        private static AlertDialog alert;

        private static Task<int?> PlatformShow(string title, string description, List<string> buttons)
        {
            tcs = new TaskCompletionSource<int?>();
            Game.Activity.RunOnUiThread(() =>
            {
                alert = new AlertDialog.Builder(Game.Activity).Create();

                alert.SetTitle(title);
                alert.SetMessage(description);

                alert.SetButton((int)DialogButtonType.Positive, buttons[0], (sender, args) =>
                {
                    if (!tcs.Task.IsCompleted)
                        tcs.SetResult(0);
                });

                if (buttons.Count > 1)
                {
                    alert.SetButton((int)DialogButtonType.Negative, buttons[1], (sender, args) =>
                    {
                        if (!tcs.Task.IsCompleted)
                            tcs.SetResult(1);
                    });
                }

                if (buttons.Count > 2)
                {
                    alert.SetButton((int)DialogButtonType.Neutral, buttons[2], (sender, args) =>
                    {
                        if (!tcs.Task.IsCompleted)
                            tcs.SetResult(2);
                    });
                }

                alert.CancelEvent += (sender, args) =>
                {
                    if (!tcs.Task.IsCompleted)
                        tcs.SetResult(null);
                };

                alert.Show();
            });

            return tcs.Task;
        }

        private static void PlatformCancel(int? result)
        {
            alert.Dismiss();
            tcs.SetResult(result);
        }
    }
}
