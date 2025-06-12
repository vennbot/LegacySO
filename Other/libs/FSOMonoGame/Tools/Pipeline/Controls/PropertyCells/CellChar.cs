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
using Eto.Forms;

namespace MonoGame.Tools.Pipeline
{
    [CellAttribute(typeof(char))]
    public class CellChar : CellBase
    {
        public override void OnCreate()
        {
            DisplayValue = ((int)(char)Value) + " (" + Value + ")";
        }

        public override void Edit(PixelLayout control)
        {
            var editText = new TextBox();
            editText.Tag = this;
            editText.Style = "OverrideSize";
            editText.Width = _lastRec.Width;
            editText.Height = _lastRec.Height;

            char value;
            char.TryParse(Value.ToString(), out value);

            editText.Text = ((int)value).ToString();

            control.Add(editText, _lastRec.X, _lastRec.Y);

            editText.Focus();
            editText.CaretIndex = editText.Text.Length;

            OnKill += delegate
            {
                OnKill = null;

                if (_eventHandler == null)
                    return;

                int num;
                if (!int.TryParse(editText.Text, out num))
                    return;

                _eventHandler((char)num, EventArgs.Empty);
            };

            editText.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Enter)
                    OnKill.Invoke();
            };
        }
    }
}
