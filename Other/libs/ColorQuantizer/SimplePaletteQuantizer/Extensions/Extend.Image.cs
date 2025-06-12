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
using System.Drawing.Imaging;
using System.Linq;

namespace SimplePaletteQuantizer.Extensions
{
    /// <summary>
    /// The utility extender class.
    /// </summary>
    public static partial class Extend
    {
        #region | Palette methods |

        /// <summary>
        /// Gets the palette color count.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public static Int32 GetPaletteColorCount(this Image image)
        {
            // checks whether a source image is valid
            if (image == null)
            {
                const String message = "Cannot assign a palette to a null image.";
                throw new ArgumentNullException(message);
            }

            // checks if the image has an indexed format
            if (!image.PixelFormat.IsIndexed())
            {
                String message = string.Format("Cannot retrieve a color count from a non-indexed image with pixel format '{0}'.", image.PixelFormat);
                throw new InvalidOperationException(message);
            }

            // returns the color count
            return image.Palette.Entries.Length;
        }

        /// <summary>
        /// Gets the palette of an indexed image.
        /// </summary>
        /// <param name="image">The source image.</param>
        public static List<Color> GetPalette(this Image image)
        {
            // checks whether a source image is valid
            if (image == null)
            {
                const String message = "Cannot assign a palette to a null image.";
                throw new ArgumentNullException(message);
            }

            // checks if the image has an indexed format
            if (!image.PixelFormat.IsIndexed())
            {
                String message = string.Format("Cannot retrieve a palette from a non-indexed image with pixel format '{0}'.", image.PixelFormat);
                throw new InvalidOperationException(message);
            }

            // retrieves and returns the palette
            return image.Palette.Entries.ToList();
        }

        /// <summary>
        /// Sets the palette of an indexed image.
        /// </summary>
        /// <param name="image">The target image.</param>
        /// <param name="palette">The palette.</param>
        public static void SetPalette(this Image image, List<Color> palette)
        {
            // checks whether a palette is valid
            if (palette == null)
            {
                const String message = "Cannot assign a null palette.";
                throw new ArgumentNullException(message);
            }

            // checks whether a target image is valid
            if (image == null)
            {
                const String message = "Cannot assign a palette to a null image.";
                throw new ArgumentNullException(message);
            }

            // checks if the image has indexed format
            if (!image.PixelFormat.IsIndexed())
            {
                String message = string.Format("Cannot store a palette to a non-indexed image with pixel format '{0}'.", image.PixelFormat);
                throw new InvalidOperationException(message);
            }

            // retrieves a target image palette
            ColorPalette imagePalette = image.Palette;

            // checks if the palette can fit into the image palette
            if (palette.Count > imagePalette.Entries.Length)
            {
                String message = string.Format("Cannot store a palette with '{0}' colors intto an image palette where only '{1}' colors are allowed.", palette.Count, imagePalette.Entries.Length);
                throw new ArgumentOutOfRangeException(message);
            }

            // copies all color entries
            for (Int32 index = 0; index < palette.Count; index++)
            {
                imagePalette.Entries[index] = palette[index];
            }

            // assigns the palette to the target image
            image.Palette = imagePalette;
        }

        #endregion
    }
}
