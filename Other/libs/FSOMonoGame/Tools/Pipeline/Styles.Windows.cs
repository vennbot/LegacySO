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

using Eto;
using Eto.Wpf.Forms;
using Eto.Wpf.Forms.Menu;
using Eto.Wpf.Forms.ToolBar;

namespace MonoGame.Tools.Pipeline
{
    public static class Styles
    {
        public static void Load()
        {
            Style.Add<MenuBarHandler>("MenuBar", h =>
            {
                h.Control.Background = System.Windows.SystemColors.ControlLightLightBrush;
            });

            Style.Add<ToolBarHandler>("ToolBar", h =>
            {
                h.Control.Background = System.Windows.SystemColors.ControlLightLightBrush;

                h.Control.Loaded += delegate
                {
                    var overflowGrid = h.Control.Template.FindName("OverflowGrid", h.Control) as System.Windows.FrameworkElement;

                    if (overflowGrid != null)
                        overflowGrid.Visibility = System.Windows.Visibility.Collapsed;

                    foreach(var item in h.Control.Items)
                    {
                        var i = item as System.Windows.Controls.Button;

                        if (i != null)
                        {
                            i.Opacity = i.IsEnabled ? 1.0 : 0.2;
                            i.IsEnabledChanged += (sender, e) => i.Opacity = i.IsEnabled ? 1.0 : 0.2;
                        }
                    }
                };
            });
        }
    }
}
