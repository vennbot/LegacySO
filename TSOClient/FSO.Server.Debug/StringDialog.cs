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
using System.Windows.Forms;

namespace tso.debug.network
{
    public partial class StringDialog : Form
    {
        public StringDialogResult Result;

        public StringDialog(string title, string description)
        {
            this.Text = title;
            InitializeComponent();
            this.description.Text = description;
        }

        private void StringModal_Load(object sender, EventArgs e)
        {
        }

        private void txtValue_Enter(object sender, EventArgs e)
        {
            btnOk.PerformClick();
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            this.btnOk.Enabled = txtValue.Text.Length > 0;
        }

        private void StringDialog_Deactivate(object sender, EventArgs e)
        {
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SetResult();
        }

        private void SetResult()
        {
            Result = new StringDialogResult { Value = txtValue.Text };
        }
    }

    public class StringDialogResult
    {
        public string Value;
    }

}
