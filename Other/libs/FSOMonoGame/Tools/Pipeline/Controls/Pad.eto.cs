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

using Eto.Drawing;
using Eto.Forms;

namespace MonoGame.Tools.Pipeline
{
#if LINUX
    public partial class Pad : GroupBox
#else
    public partial class Pad : Panel
#endif
    {
        private DynamicLayout layout;
        private StackLayout stack;
        private ImageView imageSettings;
        private Panel panelLabel;
        private Label label;

        private void InitializeComponent()
        {
            layout = new DynamicLayout();

            panelLabel = new Panel();
            panelLabel.Padding = new Padding(5);

            if (!Global.Unix)
                panelLabel.Height = 25;

            stack = new StackLayout();
            stack.Orientation = Orientation.Horizontal;

            label = new Label();
            label.Font = new Font(label.Font.Family, label.Font.Size - 1, FontStyle.Bold);
            stack.Items.Add(new StackLayoutItem(label, true));

            imageSettings = new ImageView();
            imageSettings.Image = Global.GetEtoIcon("Icons.Settings.png");
            imageSettings.Visible = false;
            stack.Items.Add(new StackLayoutItem(imageSettings, false)); 

            panelLabel.Content = stack;

            layout.AddRow(panelLabel);

            Content = layout;

            imageSettings.MouseDown += ImageSettings_MouseDown;
        }
    }
}

