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
    public partial class PropertyGridTable : Scrollable
    {
        PixelLayout pixel1;
        Drawable drawable;

        private void InitializeComponent()
        {
            BackgroundColor = DrawInfo.BackColor;

            pixel1 = new PixelLayout();
            pixel1.BackgroundColor = DrawInfo.BackColor;

            drawable = new Drawable();
            drawable.Height = 100;
            pixel1.Add(drawable, 0, 0);

            Content = pixel1;

            pixel1.Style = "Stretch";
            drawable.Style = "Stretch";

#if MONOMAC
            drawable.Width = 10;
#endif

            drawable.Paint += Drawable_Paint;
            drawable.MouseDown += Drawable_MouseDown;
            drawable.MouseUp += Drawable_MouseUp;
            drawable.MouseMove += Drawable_MouseMove;
            drawable.MouseLeave += Drawable_MouseLeave;
            SizeChanged += PropertyGridTable_SizeChanged;
        }
    }
}

