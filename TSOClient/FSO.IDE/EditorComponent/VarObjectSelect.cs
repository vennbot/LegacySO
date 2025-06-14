
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

namespace FSO.IDE.EditorComponent
{
    public partial class VarObjectSelect : Form
    {
        public uint GUIDResult;

        public VarObjectSelect()
        {
            InitializeComponent();
            Browser.SelectedChanged += Browser_SelectedChanged;
            Browser.RefreshTree();
        }

        private void Browser_SelectedChanged()
        {
            SelectButton.Enabled = (Browser.SelectedObj != null);
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (Browser.SelectedObj != null)
            {
                DialogResult = DialogResult.OK;
                GUIDResult = Browser.SelectedObj.GUID;
            }
            else
                DialogResult = DialogResult.Cancel;

            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
