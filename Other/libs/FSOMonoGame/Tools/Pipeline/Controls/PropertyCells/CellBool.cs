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
    [CellAttribute(typeof(bool))]
    public class CellBool : CellBase
    {
        private bool _draw;

        public CellBool()
        {
            _draw = true;
        }

        public override void Edit(PixelLayout control)
        {
            _draw = false;

            var checkbox = new CheckBox();
            checkbox.Tag = this;
            checkbox.Checked = (bool?)Value;
            checkbox.ThreeState = (Value == null);
            checkbox.Text = (checkbox.Checked == null) ? "Not Set" : checkbox.Checked.ToString();
            checkbox.Width = _lastRec.Width - 10;
            checkbox.Height = _lastRec.Height;
            control.Add(checkbox, _lastRec.X + 10, _lastRec.Y);

            checkbox.CheckedChanged += (sender, e) => checkbox.Text = (checkbox.Checked == null) ? "Not Set" : checkbox.Checked.ToString();

            OnKill += delegate
            {
                OnKill = null;

                if (_eventHandler == null || checkbox.Checked == null)
                    return;
                
                _draw = true;
                Value = checkbox.Checked;
                _eventHandler(Value, EventArgs.Empty);
            };
        }

        public override void DrawCell(Graphics g, Rectangle rec, int separatorPos, bool selected)
        {
            if (_draw)
                base.DrawCell(g, rec, separatorPos, selected);
        }
    }
}

