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
using System.Collections.Generic;
using System.Drawing;

namespace SimplePaletteQuantizer.ColorCaches.LocalitySensitiveHash
{
    public class BucketInfo
    {
        private readonly SortedDictionary<Int32, Color> colors;

        /// <summary>
        /// Gets the colors.
        /// </summary>
        /// <value>The colors.</value>
        public IDictionary<Int32, Color> Colors
        {
            get { return colors; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BucketInfo"/> class.
        /// </summary>
        public BucketInfo()
        {
            colors = new SortedDictionary<Int32, Color>();
        }

        /// <summary>
        /// Adds the color to the bucket informations.
        /// </summary>
        /// <param name="paletteIndex">Index of the palette.</param>
        /// <param name="color">The color.</param>
        public void AddColor(Int32 paletteIndex, Color color)
        {
            colors.Add(paletteIndex, color);
        }
    }
}
