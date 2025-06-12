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

namespace SimplePaletteQuantizer.Quantizers.Popularity
{
    internal class PopularityColorSlot
    {
        #region | Fields |

        private Int32 red;
        private Int32 green;
        private Int32 blue;

        #endregion

        #region | Properties |

        /// <summary>
        /// Gets or sets the pixel count.
        /// </summary>
        /// <value>The pixel count.</value>
        public Int32 PixelCount { get; private set; }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="PopularityColorSlot"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        public PopularityColorSlot(Color color)
        {
            AddValue(color);
        }

        #endregion

        #region | Methods |

        /// <summary>
        /// Adds the value to the slot.
        /// </summary>
        /// <param name="color">The color to be added.</param>
        public PopularityColorSlot AddValue(Color color)
        {
            red += color.R;
            green += color.G;
            blue += color.B;
            PixelCount++;
            return this;
        }

        /// <summary>
        /// Gets the average, just simple value divided by pixel presence.
        /// </summary>
        /// <returns>The average color component value.</returns>
        public Color GetAverage()
        {
            // determines the components
            Int32 finalRed = red/PixelCount;
            Int32 finalGreen = green/PixelCount;
            Int32 finalBlue = blue/PixelCount;

            // clamps the invalid values
            if (finalRed < 0) finalRed = 0;
            if (finalRed > 255) finalRed = 255;
            if (finalGreen < 0) finalGreen = 0;
            if (finalGreen > 255) finalGreen = 255;
            if (finalBlue < 0) finalBlue = 0;
            if (finalBlue > 255) finalBlue = 255;

            // returns the reconstructed color
            return Color.FromArgb(255, finalRed, finalGreen, finalBlue);
        }

        #endregion
    }
}
