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
// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System.Collections.Generic;
using Eto.Forms;

namespace MonoGame.Tools.Pipeline
{
    public partial class Pad
    {
        public string Title
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public List<Command> Commands;
        private ContextMenu _contextMenu;

        public Pad()
        {
            InitializeComponent();

            Commands = new List<Command>();
            _contextMenu = new ContextMenu();
        }

        public virtual void LoadSettings()
        {
            
        }

        private void ImageSettings_MouseDown(object sender, MouseEventArgs e)
        {
            _contextMenu.Show(imageSettings);
        }

        public void CreateContent(Control control)
        {
            layout.AddRow(control);
        }

        public void AddCommand(Command com)
        {
            imageSettings.Visible = true;

            Commands.Add(com);
            _contextMenu.Items.Add(com.CreateMenuItem());
        }

        public void AddCommand(RadioCommand com)
        {
            imageSettings.Visible = true;

            Commands.Add(com);
            _contextMenu.Items.Add(com);
        }
    }
}

