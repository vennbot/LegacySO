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
    public class BayerDitherer4 : BaseOrderedDitherer
    {
        /// <summary>
        /// See <see cref="BaseColorDitherer.CreateCoeficientMatrix"/> for more details.
        /// </summary>
        protected override Byte[,] CreateCoeficientMatrix()
        {
            return new Byte[,] 
            {
        		{  1,  9,  3, 11 },
			    { 13,  5, 15,  7 },
			    {  4, 12,  2, 10 },
			    { 16,  8, 14,  6 }
            };
        }

        /// <summary>
        /// See <see cref="BaseOrderedDitherer.MatrixWidth"/> for more details.
        /// </summary>
        protected override Byte MatrixWidth
        {
            get { return 4; }
        }

        /// <summary>
        /// See <see cref="BaseOrderedDitherer.MatrixHeight"/> for more details.
        /// </summary>
        protected override Byte MatrixHeight
        {
            get { return 4; }
        }
    }
}
