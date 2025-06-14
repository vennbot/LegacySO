
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
using FSO.Content;
using FSO.IDE.Managers;
using System;
using System.IO;
using System.Windows.Forms;

namespace FSO.IDE.ResourceBrowser
{
    public partial class IffResourceViewer : Form, IffResWindow
    {
        private GameIffResource Iff;

        public IffResourceViewer()
        {
            InitializeComponent();
        }

        public IffResourceViewer(string name, GameIffResource iff, GameObject srcObj) : this()
        {
            Iff = iff;
            iffRes.Init();
            iffRes.ChangeIffSource(iff);
            iffRes.ChangeActiveObject(srcObj);
            SetTab(0);

            Text = "Iff Editor - " + name;
        }

        public void SetTargetObject(GameObject obj)
        {
            iffRes.ChangeActiveObject(obj);
        }

        private void piffDebugButton_Click(object sender, EventArgs e)
        {
            var test = FSO.Files.Formats.PiffEncoder.GeneratePiff(iffRes.ActiveIff.MainIff, null, null, null);
            var filename = "Content/Patch/" + test.Filename;
            Directory.CreateDirectory(Path.GetDirectoryName(filename));
            using (var stream = new FileStream(filename, FileMode.Create))
                test.Write(stream);
        }

        private void IffResourceViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainWindow.Instance.IffManager.CloseResourceWindow(iffRes.ActiveIff);
        }

        private void SetTab(int i)
        {
            iffRes.Visible = i == 0;
            piffEditor.Visible = i == 1;

            resourcesToolStripMenuItem.CheckState = (i == 0) ? CheckState.Indeterminate : CheckState.Unchecked;
            patchesPIFFToolStripMenuItem.CheckState = (i == 1) ? CheckState.Indeterminate : CheckState.Unchecked;

            piffEditor.SetActiveIff(Iff);
        }

        private void resourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTab(0);
        }

        private void patchesPIFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTab(1);
        }
    }
}
