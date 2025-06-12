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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSO.Client.Debug
{
    public partial class FSOExceptionDisplay : Form
    {
        public FSOExceptionDisplay()
        {
            InitializeComponent();
        }

        public FSOExceptionDisplay(string trace) : this()
        {
            ExceptionBox.Text = trace;
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            var text = ExceptionBox.Text;
            var t = new Thread(() =>
            {
                Clipboard.SetText(text);
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
    }
}
