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
using System.Runtime.InteropServices;

namespace SimplePaletteQuantizer.Helpers.Pixels.Indexed
{
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct PixelData1Indexed : IIndexedPixel
    {
        // raw component values
        private Byte index;

        // get - index method
        public Byte GetIndex(Int32 offset)
        {
            return (index & 1 << (7 - offset)) != 0 ? Pixel.One : Pixel.Zero;
        }

        // set - index method
        public void SetIndex(Int32 offset, Byte value)
        {
            value = value == 0 ? Pixel.One : Pixel.Zero;

            if (value == 0)
            {
                index |= (Byte) (1 << (7 - offset));
            }
            else
            {
                index &= (Byte) (~(1 << (7 - offset)));
            }
        }
    }
}
