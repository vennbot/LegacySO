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

namespace SimplePaletteQuantizer.Helpers
{
    public class QuantizationHelper
    {
        private const int Alpha = 255 << 24;
        private static readonly Color BackgroundColor;
        private static readonly Double[] Factors;

        static QuantizationHelper()
        {
            BackgroundColor = SystemColors.Control;
            Factors = PrecalculateFactors();
        }

        /// <summary>
        /// Precalculates the alpha-fix values for all the possible alpha values (0-255).
        /// </summary>
        private static Double[] PrecalculateFactors()
        {
            Double[] result = new Double[256];

            for (Int32 value = 0; value < 256; value++)
            {
                result[value] = value / 255.0;
            }

            return result;
        }

        /// <summary>
        /// Converts the alpha blended color to a non-alpha blended color.
        /// </summary>
        /// <param name="color">The alpha blended color (ARGB).</param>
        /// <returns>The non-alpha blended color (RGB).</returns>
        internal static Color ConvertAlpha(Color color)
        {
            Int32 argb;
            return ConvertAlpha(color, out argb);
        }

        /// <summary>
        /// Converts the alpha blended color to a non-alpha blended color.
        /// </summary>
        internal static Color ConvertAlpha(Color color, out Int32 argb)
        {
            Color result = color;

            if (color.A < 255)
            {
                // performs a alpha blending (second color is BackgroundColor, by default a Control color)
                Double colorFactor = Factors[color.A];
                Double backgroundFactor = Factors[255 - color.A];
                Int32 red = (Int32) (color.R*colorFactor + BackgroundColor.R*backgroundFactor);
                Int32 green = (Int32) (color.G*colorFactor + BackgroundColor.G*backgroundFactor);
                Int32 blue = (Int32) (color.B*colorFactor + BackgroundColor.B*backgroundFactor);
                argb = red << 16 | green << 8 | blue;
                Color.FromArgb(red, green, blue);
                result = Color.FromArgb(Alpha | argb);
            }
            else
            {
                argb = color.R << 16 | color.G << 8 | color.B;
            }

            return result;
        }
    }
}
