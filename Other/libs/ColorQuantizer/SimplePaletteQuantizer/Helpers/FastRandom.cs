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

namespace SimplePaletteQuantizer.Helpers
{
    public class FastRandom
    {
        private const Double RealUnitInt = 1.0/(Int32.MaxValue + 1.0);

        private UInt32 x, y, z, w;

        public FastRandom(UInt32 seed)
        {
            x = seed;
            y = 842502087;
            z = 3579807591;
            w = 273326509;
        }

        public Int32 Next(Int32 upperBound)
        {
            UInt32 t = (x ^ (x << 11)); x = y; y = z; z = w;
            return (Int32) ((RealUnitInt*(Int32) (0x7FFFFFFF & (w = (w ^ (w >> 19)) ^ (t ^ (t >> 8)))))*upperBound);
        }
    }
}
