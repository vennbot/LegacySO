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
    partial class EditDialog : Dialog<bool>
    {
        DynamicLayout layout1;
        Label label1, label2;
        TextBox textBox1;
        Button buttonOk, buttonCancel;

        private void InitializeComponent()
        {
            DisplayMode = DialogDisplayMode.Attached;
            Size = new Size(400, 160);

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
            layout1.Padding = new Padding(6);
            layout1.BeginVertical();

            layout1.Add(null, true, true);

            label1 = new Label();
            layout1.Add(label1);

            textBox1 = new TextBox();
            layout1.Add(textBox1);
            
            label2 = new Label();
            label2.TextColor = new Color(SystemColors.ControlText, 0.5f);
            label2.TextAlignment = TextAlignment.Center;
            layout1.Add(label2);

            layout1.Add(null, true, true);

            layout1.EndVertical();
            Content = layout1;

            textBox1.TextChanged += TextBox1_TextChanged;
            buttonOk.Click += ButtonOk_Click;
            buttonCancel.Click += ButtonCancel_Click;
        }
    }
}
