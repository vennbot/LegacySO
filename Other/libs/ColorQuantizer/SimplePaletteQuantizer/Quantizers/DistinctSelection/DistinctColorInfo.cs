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
using System.Drawing;

namespace SimplePaletteQuantizer.Quantizers.DistinctSelection
{
    /// <summary>
    /// Stores all the informations about single color only once, to be used later.
    /// </summary>
    public class DistinctColorInfo
    {
        private const Int32 Factor = 5000000;

        /// <summary>
        /// The original color.
        /// </summary>
        public Int32 Color { get; private set; }

        /// <summary>
        /// The pixel presence count in the image.
        /// </summary>
        public Int32 Count { get; private set; }

        /// <summary>
        /// A hue component of the color.
        /// </summary>
        public Int32 Hue { get; private set; }

        /// <summary>
        /// A saturation component of the color.
        /// </summary>
        public Int32 Saturation { get; private set; }

        /// <summary>
        /// A brightness component of the color.
        /// </summary>
        public Int32 Brightness { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistinctColorInfo"/> struct.
        /// </summary>
        public DistinctColorInfo(Color color)
        {
            Color = color.ToArgb();
            Count = 1;

            Hue = Convert.ToInt32(color.GetHue()*Factor);
            Saturation = Convert.ToInt32(color.GetSaturation()*Factor);
            Brightness = Convert.ToInt32(color.GetBrightness()*Factor);
        }

        /// <summary>
        /// Increases the count of pixels of this color.
        /// </summary>
        public DistinctColorInfo IncreaseCount()
        {
            Count++;
            return this;
        }
    }
}
