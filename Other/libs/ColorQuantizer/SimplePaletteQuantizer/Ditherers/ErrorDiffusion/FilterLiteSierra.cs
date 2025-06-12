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

namespace SimplePaletteQuantizer.Ditherers.ErrorDiffusion
{
    public class FilterLiteSierra : BaseErrorDistributionDitherer
    {
        /// <summary>
        /// See <see cref="BaseColorDitherer.CreateCoeficientMatrix"/> for more details.
        /// </summary>
        protected override Byte[,] CreateCoeficientMatrix()
        {
            return new Byte[,]
            {
                { 0, 0, 0 },
                { 0, 0, 2 },
                { 1, 1, 0 }
            };
        }

        /// <summary>
        /// See <see cref="BaseErrorDistributionDitherer.MatrixSideWidth"/> for more details.
        /// </summary>
        protected override Int32 MatrixSideWidth
        {
            get { return 1; }
        }

        /// <summary>
        /// See <see cref="BaseErrorDistributionDitherer.MatrixSideHeight"/> for more details.
        /// </summary>
        protected override Int32 MatrixSideHeight
        {
            get { return 1; }
        }
    }
}
