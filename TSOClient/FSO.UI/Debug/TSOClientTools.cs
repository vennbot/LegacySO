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
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace FSO.Client.Debug
{
    public partial class TSOClientTools : Form
    {
        private TSOClientUIInspector uiInspetor;
        private TSOSceneInspector sceneInspector;

        public TSOClientTools()
        {
            InitializeComponent();

            /**
             * UI Inspector
             */
            uiInspetor = new TSOClientUIInspector();
            uiInspetor.Show();

            sceneInspector = new TSOSceneInspector();
            sceneInspector.Show();


        }

        public void PositionAroundGame(GameWindow gameWindow)
        {

            this.Location = new System.Drawing.Point(gameWindow.ClientBounds.X - this.Width - 10, gameWindow.ClientBounds.Y);
            uiInspetor.Location = new System.Drawing.Point(
                gameWindow.ClientBounds.X - uiInspetor.Width - 10,
                gameWindow.ClientBounds.Y + this.Height + 10
            );
            sceneInspector.Location = new System.Drawing.Point(
                gameWindow.ClientBounds.X + gameWindow.ClientBounds.Width + 10,
                gameWindow.ClientBounds.Y
            );
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var window = new TSOClientFindAssetSearch();
            window.StartSearch(txtFindAsset.Text);
            window.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var window = new TSOEdith();
            //window.Show();
        }
    }
}
