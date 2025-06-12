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
using Microsoft.Xna.Framework.Graphics;
using Windows.UI.Xaml.Controls;

namespace Microsoft.Xna.Framework
{
    partial class GraphicsDeviceManager
    {
        [CLSCompliant(false)] 
        public SwapChainPanel SwapChainPanel { get; set; }

        partial void PlatformPreparePresentationParameters(PresentationParameters presentationParameters)
        {

            // The graphics device can use a XAML panel or a window
            // to created the default swapchain target.
            if (SwapChainPanel != null)
            {
                presentationParameters.DeviceWindowHandle = IntPtr.Zero;
                presentationParameters.SwapChainPanel = this.SwapChainPanel;
           }
            else
            {
                presentationParameters.DeviceWindowHandle = _game.Window.Handle;
                presentationParameters.SwapChainPanel = null;
            }
        }
    }
}
