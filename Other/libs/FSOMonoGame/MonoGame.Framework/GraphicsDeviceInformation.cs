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

using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// The settings used in creation of the graphics device.
    /// See <see cref="GraphicsDeviceManager.PreparingDeviceSettings"/>.
    /// </summary>
    public class GraphicsDeviceInformation
    {	
        /// <summary>
        /// The graphics adapter on which the graphics device will be created.
        /// </summary>
        /// <remarks>
        /// This is only valid on desktop systems where multiple graphics 
        /// adapters are possible.  Defaults to <see cref="GraphicsAdapter.DefaultAdapter"/>.
        /// </remarks>
        public GraphicsAdapter Adapter { get; set; }
        
        /// <summary>
        /// The requested graphics device feature set. 
        /// </summary>
        public GraphicsProfile GraphicsProfile { get; set; }
        
        /// <summary>
        /// The settings that define how graphics will be presented to the display.
        /// </summary>
        public PresentationParameters PresentationParameters { get; set; }
    }
}

