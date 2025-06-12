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
    public struct PixelData4Indexed : IIndexedPixel
    {
        // raw component values
        private Byte index;

        // get - index method
        public Byte GetIndex(Int32 offset)
        {
            return (Byte) GetBitRange(8 - offset - 4, 7 - offset);
        }

        // set - index method
        public void SetIndex(Int32 offset, Byte value)
        {
            SetBitRange(8 - offset - 4, 7 - offset, value);
        }

        private Int32 GetBitRange(Int32 startOffset, Int32 endOffset)
        {
            Int32 result = 0;
            Byte bitIndex = 0;

            for (Int32 offset = startOffset; offset <= endOffset; offset++)
            {
                Int32 bitValue = 1 << bitIndex;
                result += GetBit(offset) ? bitValue : 0;
                bitIndex++;
            }

            return result;
        }

        private Boolean GetBit(Int32 offset)
        {
            return (index & (1 << offset)) != 0;
        }

        private void SetBitRange(Int32 startOffset, Int32 endOffset, Int32 value)
        {
            Byte bitIndex = 0;

            for (Int32 offset = startOffset; offset <= endOffset; offset++)
            {
                Int32 bitValue = 1 << bitIndex;
                SetBit(offset, (value & bitValue) != 0);
                bitIndex++;
            }
        }

        private void SetBit(Int32 offset, Boolean value)
        {
            if (value)
            {
                index |= (Byte) (1 << offset);
            }
            else
            {
                index &= (Byte) (~(1 << offset));
            }
        }
    }
}
