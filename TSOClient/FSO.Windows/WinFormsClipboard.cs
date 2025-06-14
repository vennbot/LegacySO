
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
using FSO.Common.Rendering.Framework.IO;
using System;
using System.Threading;

namespace FSO.Windows
{
    public class WinFormsClipboard : ClipboardHandler
    {
        public override string Get()
        {
            var wait = new AutoResetEvent(false);
            string clipboardText = "";
            
            var clipThread = new Thread(x =>
            {
                clipboardText = System.Windows.Forms.Clipboard.GetText(System.Windows.Forms.TextDataFormat.UnicodeText);
                wait.Set();
            });
            clipThread.SetApartmentState(ApartmentState.STA);
            clipThread.Start();
            
            wait.WaitOne();
            
            return clipboardText;
        }

        public override void Set(string str)
        {
            var copyThread = new Thread(x =>
            {
                System.Windows.Forms.Clipboard.SetText((String.IsNullOrEmpty(str)) ? " " : str);
            });
            copyThread.SetApartmentState(ApartmentState.STA);
            copyThread.Start();
        }
    }
}
