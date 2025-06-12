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
    partial class ReferenceDialog : Dialog<bool>
    {
        DynamicLayout layout1;
        StackLayout stack1;
        GridView grid1;
        Button buttonAdd, buttonRemove;
        Button buttonOk, buttonCancel;

        private void InitializeComponent()
        {
            Title = "Reference Editor";
            DisplayMode = DialogDisplayMode.Attached;
            Resizable = true;
            Padding = new Padding(4);
            Size = new Size(500, 400);
            MinimumSize = new Size(450, 300);

            buttonOk = new Button();
            buttonOk.Text = "Ok";
            PositiveButtons.Add(buttonOk);
            DefaultButton = buttonOk;

            buttonCancel = new Button();
            buttonCancel.Text = "Cancel";
            NegativeButtons.Add(buttonCancel);
            AbortButton = buttonCancel;

            layout1 = new DynamicLayout();
            layout1.DefaultSpacing = new Size(4, 4);
            layout1.BeginHorizontal();

            grid1 = new GridView();
            grid1.Style = "GridView";
            layout1.Add(grid1, true, true);

            stack1 = new StackLayout();
            stack1.Orientation = Orientation.Vertical;
            stack1.Spacing = 4;

            buttonAdd = new Button();
            buttonAdd.Text = "Add";
            stack1.Items.Add(new StackLayoutItem(buttonAdd, false));

            buttonRemove = new Button();
            buttonRemove.Text = "Remove";
            stack1.Items.Add(new StackLayoutItem(buttonRemove, false));

            layout1.Add(stack1, false, true);

            Content = layout1;

            grid1.SelectionChanged += Grid1_SelectionChanged;
            grid1.KeyDown += Grid1_KeyDown;
            buttonAdd.Click += ButtonAdd_Click;
            buttonRemove.Click += ButtonRemove_Click;
            buttonOk.Click += ButtonOk_Click;
            buttonCancel.Click += ButtonCancel_Click;
        }
    }
}
