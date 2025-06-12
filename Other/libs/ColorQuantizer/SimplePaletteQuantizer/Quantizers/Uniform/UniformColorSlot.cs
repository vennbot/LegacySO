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

namespace SimplePaletteQuantizer.Quantizers.Uniform
{
    internal struct UniformColorSlot
    {
        private Int32 value;
        private Int32 pixelCount;

        /// <summary>
        /// Adds the value to the slot.
        /// </summary>
        /// <param name="component">The color component value.</param>
        public void AddValue(Int32 component)
        {
            value += component;
            pixelCount++;
        }

        /// <summary>
        /// Gets the average, just simple value divided by pixel presence.
        /// </summary>
        /// <returns>The average color component value.</returns>
        public Int32 GetAverage()
        {
            Int32 result = 0;

            if (pixelCount > 0)
            {
                result = pixelCount == 1 ? value : value/pixelCount;
            }

            return result;
        }
    }
}
