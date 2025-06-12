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
using System.ComponentModel;
using Eto.Forms;

namespace MonoGame.Tools.Pipeline
{
    [CellAttribute(typeof(short))]
    [CellAttribute(typeof(int))]
    [CellAttribute(typeof(long))]
    [CellAttribute(typeof(ushort))]
    [CellAttribute(typeof(uint))]
    [CellAttribute(typeof(ulong))]
    [CellAttribute(typeof(float))]
    [CellAttribute(typeof(double))]
    [CellAttribute(typeof(decimal))]
    public class CellNumber : CellBase
    {
        private TypeConverter _converter;

        public override void OnCreate()
        {
            _converter = TypeDescriptor.GetConverter(_type);

            if (_type == typeof(float) || _type == typeof(double) || _type == typeof(decimal))
            {
                if (_type == typeof(float))
                    DisplayValue = ((float)Value).ToString("0.00");
                else if (_type == typeof(double))
                    DisplayValue = ((double)Value).ToString("0.00");
                else
                    DisplayValue = ((decimal)Value).ToString("0.00");

                DisplayValue = (DisplayValue.Length > Value.ToString().Length) ? DisplayValue : Value.ToString();
            }
        }

        public override void Edit(PixelLayout control)
        {
            var editText = new TextBox();
            editText.Tag = this;
            editText.Style = "OverrideSize";
            editText.Width = _lastRec.Width;
            editText.Height = _lastRec.Height;
            editText.Text = DisplayValue;
            editText.Tag = this;

            control.Add(editText, _lastRec.X, _lastRec.Y);

            editText.Focus();
            editText.CaretIndex = editText.Text.Length;

            OnKill += delegate
            {
                OnKill = null;

                if (_eventHandler == null)
                    return;

                try
                {
                    _eventHandler(_converter.ConvertFrom(editText.Text), EventArgs.Empty);
                }
                catch { }
            };

            editText.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Enter)
                    OnKill.Invoke();
            };
        }
    }
}

