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
using SimplePaletteQuantizer.Helpers;
using SimplePaletteQuantizer.PathProviders;

namespace SimplePaletteQuantizer.Quantizers
{
    /// <summary>
    /// This interface provides a color quantization capabilities.
    /// </summary>
    public interface IColorQuantizer : IPathProvider
    {
        /// <summary>
        /// Gets a value indicating whether to allow parallel processing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if to allow parallel processing; otherwise, <c>false</c>.
        /// </value>
        Boolean AllowParallel { get; }

        /// <summary>
        /// Prepares the quantizer for image processing.
        /// </summary>
        void Prepare(ImageBuffer image);

        /// <summary>
        /// Adds the color to quantizer.
        /// </summary>
        void AddColor(Color color, Int32 x, Int32 y);

        /// <summary>
        /// Gets the palette with specified count of the colors.
        /// </summary>
        List<Color> GetPalette(Int32 colorCount);

        /// <summary>
        /// Gets the index of the palette for specific color.
        /// </summary>
        Int32 GetPaletteIndex(Color color, Int32 x, Int32 y);

        /// <summary>
        /// Gets the color count.
        /// </summary>
        Int32 GetColorCount();

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Finish();
    }
}
