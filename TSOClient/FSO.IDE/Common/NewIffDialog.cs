
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
using FSO.Common;
using FSO.Files.Formats.IFF;
using System;
using System.IO;
using System.Windows.Forms;

namespace FSO.IDE.Common
{
    public partial class NewIffDialog : Form
    {
        public IffFile InitIff = null;
        public NewIffDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            var name = Path.Combine(FSOEnvironment.ContentDir, "Objects/" +NameEntry.Text+".iff");
            var objProvider = Content.Content.Get().WorldObjects;
            if (NameEntry.Text == "")
            {
                MessageBox.Show("Name cannot be empty!", "Invalid IFF Name");
            }
            else
            {
                //search for duplicates.
                lock (objProvider.Entries)
                {
                    foreach (var obj in objProvider.Entries.Values)
                    {
                        if (obj.FileName == name)
                        {
                            MessageBox.Show("Name "+name+" already taken!", "Invalid IFF Name");
                            return;
                        }
                    }
                }
                //we're good. Create the IFF and add it. Don't drop the lock, so changes cannot be made between this check.
                var iff = new IffFile();
                iff.RuntimeInfo.Path = name;
                iff.RuntimeInfo.State = IffRuntimeState.Standalone;
                iff.Filename = NameEntry.Text;

                DialogResult = DialogResult.OK;
                InitIff = iff;
                Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
