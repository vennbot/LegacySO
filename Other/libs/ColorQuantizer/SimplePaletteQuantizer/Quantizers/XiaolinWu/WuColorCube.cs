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
using System;

namespace SimplePaletteQuantizer.Quantizers.XiaolinWu
{
    internal class WuColorCube
    {
        /// <summary>
        /// Gets or sets the red minimum.
        /// </summary>
        /// <value>The red minimum.</value>
        public Int32 RedMinimum { get; set; }

        /// <summary>
        /// Gets or sets the red maximum.
        /// </summary>
        /// <value>The red maximum.</value>
        public Int32 RedMaximum { get; set; }

        /// <summary>
        /// Gets or sets the green minimum.
        /// </summary>
        /// <value>The green minimum.</value>
        public Int32 GreenMinimum { get; set; }

        /// <summary>
        /// Gets or sets the green maximum.
        /// </summary>
        /// <value>The green maximum.</value>
        public Int32 GreenMaximum { get; set; }

        /// <summary>
        /// Gets or sets the blue minimum.
        /// </summary>
        /// <value>The blue minimum.</value>
        public Int32 BlueMinimum { get; set; }

        /// <summary>
        /// Gets or sets the blue maximum.
        /// </summary>
        /// <value>The blue maximum.</value>
        public Int32 BlueMaximum { get; set; }

        /// <summary>
        /// Gets or sets the cube volume.
        /// </summary>
        /// <value>The volume.</value>
        public Int32 Volume { get; set; }
    }
}
