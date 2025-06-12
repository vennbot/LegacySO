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
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace Microsoft.Xna.Framework.Input
{
    public static partial class MessageBox
    {
        private static readonly CoreDispatcher dispatcher;
        private static TaskCompletionSource<int?> tcs;
        private static IAsyncOperation<IUICommand> dialogResult; 

        static MessageBox()
        {
            dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
        }

        private static Task<int?> PlatformShow(string title, string description, List<string> buttons)
        {
            // TODO: MessageDialog only supports two buttons
            if (buttons.Count == 3)
                throw new NotSupportedException("This platform does not support three buttons");

            tcs = new TaskCompletionSource<int?>();

            MessageDialog dialog = new MessageDialog(description, title);
            foreach (string button in buttons)
                dialog.Commands.Add(new UICommand(button, null, dialog.Commands.Count));

            dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                async () =>
                {
                    try
                    {
                        // PlatformSetResult will cancel the task, resulting in an exception
                        dialogResult = dialog.ShowAsync();
                        var result = await dialogResult;
                        if (!tcs.Task.IsCompleted)
                            tcs.SetResult(result == null ? null : (int?)result.Id);
                    }
                    catch (TaskCanceledException)
                    {
                        if (!tcs.Task.IsCompleted)
                            tcs.SetResult(null);
                    }
                });

            return tcs.Task;
        }

        private static void PlatformCancel(int? result)
        {
            // TODO: MessageDialog doesn't hide on Windows Phone 8.1
            tcs.SetResult(result);
            dialogResult.Cancel();
        }
    }
}
