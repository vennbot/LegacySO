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
namespace SimplePaletteQuantizer.Helpers
{
    public class PixelTransform
    {
        /// <summary>
        /// Gets the source pixel.
        /// </summary>
        public Pixel SourcePixel { get; private set; }

        /// <summary>
        /// Gets the target pixel.
        /// </summary>
        public Pixel TargetPixel { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PixelTransform"/> class.
        /// </summary>
        public PixelTransform(Pixel sourcePixel, Pixel targetPixel)
        {
            SourcePixel = sourcePixel;
            TargetPixel = targetPixel;
        }
    }
}
