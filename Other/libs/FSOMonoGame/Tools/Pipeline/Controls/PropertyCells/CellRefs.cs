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
using System.IO;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;

namespace MonoGame.Tools.Pipeline
{
    [CellAttribute(typeof(List<string>), Name = "References")]
    public class CellRefs : CellBase
    {
        public override void OnCreate()
        {
            if (Value == null)
                Value = new List<string>();

            var list = Value as List<string>;
            var displayValue = "";

            foreach (var value in list)
                displayValue += Environment.NewLine + Path.GetFileNameWithoutExtension (value);

            DisplayValue = (Value as List<string>).Count > 0 ? displayValue.Trim(Environment.NewLine.ToCharArray()) : "None";
            Height = Height * Math.Max(list.Count, 1);
        }

        public override void Edit(PixelLayout control)
        {
            var dialog = new ReferenceDialog(PipelineController.Instance, (Value as List<string>).ToArray());
            if (dialog.ShowModal(control) && _eventHandler != null)
            {
                _eventHandler(dialog.References, EventArgs.Empty);
                PipelineController.Instance.OnReferencesModified();
            }
        }
    }
}

