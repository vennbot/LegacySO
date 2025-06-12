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
using System.Threading;

namespace FSO.IDE.Utils
{
    public static class FormsUtils
    {
        public static void StaExecute(Action action)
        {
            // ? ? ?_? ?? IMPEACH STAThread ? ? ?_? ??
            var wait = new AutoResetEvent(false);
            var thread = new Thread(() => {
                action();
                wait.Set();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            wait.WaitOne();
            return;
        }
    }
}
