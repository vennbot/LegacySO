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

using System;
using Eto.Drawing;
using Eto.Forms;

namespace MonoGame.Tools.Pipeline
{
    partial class EditDialog : Dialog<bool>
    {
        public string Text { get; private set; }

        private string _errInvalidName;
        private bool _file;

        public EditDialog(string title, string label, string text, bool file)
        {
            InitializeComponent();

            _errInvalidName = "The following characters are not allowed:";
            for (int i = 0; i < Global.NotAllowedCharacters.Length; i++)
                _errInvalidName += " " + Global.NotAllowedCharacters[i];
            
            Title = title;
            label1.Text = label;
            textBox1.Text = text;

            Text = text;
            _file = file;

            TextBox1_TextChanged(this, EventArgs.Empty);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var index = textBox1.Text.IndexOf('.');
            if (_file && index != -1)
                textBox1.Selection = new Range<int>(0, index - 1);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!_file)
                return;

            var stringOk = Global.CheckString(textBox1.Text);

            DefaultButton.Enabled = (stringOk && (textBox1.Text != ""));
            label2.Text = !stringOk ? _errInvalidName : "";

            Text = textBox1.Text;
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            Result = true;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
