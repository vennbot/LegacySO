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
    partial class DeleteDialog : Dialog<bool>
    {
        DynamicLayout layout1;
        Label label1;
        TreeGridView treeView1;
        Button buttonDelete, buttonCancel;

        private void InitializeComponent()
        {
            Title = "Delete Items";
            DisplayMode = DialogDisplayMode.Attached;
            Resizable = true;
            Size = new Size(450, 300);
            MinimumSize = new Size(350, 250);

            buttonDelete = new Button();
            buttonDelete.Text = "Delete";
            PositiveButtons.Add(buttonDelete);
            DefaultButton = buttonDelete;
            buttonDelete.Style = "Destuctive";

            buttonCancel = new Button();
            buttonCancel.Text = "Cancel";
            NegativeButtons.Add(buttonCancel);
            AbortButton = buttonCancel;

            layout1 = new DynamicLayout();
            layout1.DefaultSpacing = new Size(2, 2);
            layout1.BeginVertical();

            label1 = new Label();
            label1.Wrap = WrapMode.Word;
            label1.Text = "The following items will be deleted (this action cannot be undone):";
            layout1.Add(label1, true, false);

            treeView1 = new TreeGridView();
            treeView1.ShowHeader = false;
            layout1.Add(treeView1, true, true);

            DefaultButton.Text = "Delete";

            Content = layout1;

            buttonDelete.Click += ButtonDelete_Click;
            buttonCancel.Click += ButtonCancel_Click;
        }
    }
}
