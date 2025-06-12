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
    partial class AddItemDialog : Dialog<bool>
    {
        DynamicLayout layout1;
        Label label1;
        RadioButton radioCopy, radioLink, radioSkip;
        CheckBox checkBox1;
        Button buttonAdd, buttonCancel;

        private void InitializeComponent()
        {
            DisplayMode = DialogDisplayMode.Attached;
            Height = 250;

            buttonAdd = new Button();
            buttonAdd.Text = "Add";
            PositiveButtons.Add(buttonAdd);
            DefaultButton = buttonAdd;

            buttonCancel = new Button();
            buttonCancel.Text = "Cancel";
            NegativeButtons.Add(buttonCancel);
            AbortButton = buttonCancel;

            layout1 = new DynamicLayout();
            layout1.DefaultSpacing = new Size(8, 8);
            layout1.Padding = new Padding(6);
            layout1.BeginVertical();

            label1 = new Label();
            label1.Wrap = WrapMode.Word;
            label1.Style = "Wrap";
            layout1.AddRow(label1);

            radioCopy = new RadioButton();
            radioCopy.Checked = true;
            layout1.AddRow(radioCopy);

            radioLink = new RadioButton(radioCopy);
            layout1.AddRow(radioLink);

            radioSkip = new RadioButton(radioCopy);
            layout1.AddRow(radioSkip);

            var spacing = new Label();
            spacing.Height = 15;
            layout1.Add(spacing, true, true);

            checkBox1 = new CheckBox();
            layout1.AddRow(checkBox1);

            layout1.EndVertical();
            Content = layout1;

            radioCopy.CheckedChanged += RadioButton_CheckedChanged;
            radioLink.CheckedChanged += RadioButton_CheckedChanged;
            radioSkip.CheckedChanged += RadioButton_CheckedChanged;
            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
            buttonAdd.Click += ButtonOk_Click;
            buttonCancel.Click += ButtonAdd_Click;
        }
    }
}
