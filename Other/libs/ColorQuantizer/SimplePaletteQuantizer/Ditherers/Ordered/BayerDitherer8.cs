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

namespace SimplePaletteQuantizer.Ditherers.Ordered
{
    public class BayerDitherer8 : BaseOrderedDitherer
    {
        /// <summary>
        /// See <see cref="BaseColorDitherer.CreateCoeficientMatrix"/> for more details.
        /// </summary>
        protected override Byte[,] CreateCoeficientMatrix()
        {
            return new Byte[,] 
            {
                {  1, 49, 13, 61,  4, 52, 16, 64 },
                { 33, 17, 45, 29, 36, 20, 48, 32 },
                {  9, 57,  5, 53, 12, 60,  8, 56 },
                { 41, 25, 37, 21, 44, 28, 40, 24 },
                {  3, 51, 15, 63,  2, 50, 14, 62 },
                { 35, 19, 47, 31, 34, 18, 46, 30 },
                { 11, 59,  7, 55, 10, 58,  6, 54 },
                { 43, 27, 39, 23, 42, 26, 38, 22 }
            };
        }

        /// <summary>
        /// See <see cref="BaseOrderedDitherer.MatrixWidth"/> for more details.
        /// </summary>
        protected override Byte MatrixWidth
        {
            get { return 8; }
        }

        /// <summary>
        /// See <see cref="BaseOrderedDitherer.MatrixHeight"/> for more details.
        /// </summary>
        protected override Byte MatrixHeight
        {
            get { return 8; }
        }
    }
}
