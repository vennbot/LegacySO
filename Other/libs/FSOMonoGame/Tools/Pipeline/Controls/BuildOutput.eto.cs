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
    public partial class BuildOutput : Pad
    {
        Panel panel;
        TextArea textArea;
        Scrollable scrollable;
        Drawable drawable;

        private void InitializeComponent()
        {
            Title = "Build Output";

            panel = new Panel();

            textArea = new TextArea();
            textArea.Wrap = false;
            textArea.ReadOnly = true;

            scrollable = new Scrollable();
            scrollable.BackgroundColor = DrawInfo.BackColor;
            scrollable.ExpandContentWidth = true;
            scrollable.ExpandContentHeight = true;
            drawable = new Drawable();
            scrollable.Content = drawable;

            panel.Content = textArea;
            CreateContent(panel);

            drawable.MouseDown += Drawable_MouseDown;
            drawable.MouseMove += Drawable_MouseMove;
            drawable.MouseLeave += Drawable_MouseLeave;
            drawable.SizeChanged += Drawable_SizeChanged;
            drawable.Paint += Drawable_Paint;
            scrollable.SizeChanged += Scrollable1_SizeChanged;
            scrollable.Scroll += Scrollable1_Scroll;
        }
    }
}
