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
extern alias MicrosoftXnaFramework;
extern alias MicrosoftXnaGamerServices;
using MsXna_Guide = MicrosoftXnaGamerServices::Microsoft.Xna.Framework.GamerServices.Guide;
using MsXna_MessageBoxIcon = MicrosoftXnaGamerServices::Microsoft.Xna.Framework.GamerServices.MessageBoxIcon;
using MsXna_PlayerIndex = MicrosoftXnaFramework::Microsoft.Xna.Framework.PlayerIndex;

using System;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Input
{
    public static partial class KeyboardInput
    {
        private static TaskCompletionSource<string> tcs;

        private static Task<string> PlatformShow(string title, string description, string defaultText, bool usePasswordMode)
        {
            tcs = new TaskCompletionSource<string>();
            MsXna_Guide.BeginShowKeyboardInput(MsXna_PlayerIndex.One, title, description, defaultText,
                ar =>
                {
                    var result = MsXna_Guide.EndShowKeyboardInput(ar);

                    if (!tcs.Task.IsCompleted)
                        tcs.SetResult(result);
                },
                null,
                usePasswordMode);

            return tcs.Task;
        }

        private static void PlatformCancel(string result)
        {
            throw new NotSupportedException();
        }
    }
}
