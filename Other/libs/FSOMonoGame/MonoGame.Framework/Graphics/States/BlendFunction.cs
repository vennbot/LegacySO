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
// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Defines a function for color blending.
    /// </summary>
	public enum BlendFunction
	{
        /// <summary>
        /// The function will adds destination to the source. (srcColor * srcBlend) + (destColor * destBlend)
        /// </summary>
		Add,
        /// <summary>
        /// The function will subtracts destination from source. (srcColor * srcBlend) − (destColor * destBlend)
        /// </summary>
		Subtract,
        /// <summary>
        /// The function will subtracts source from destination. (destColor * destBlend) - (srcColor * srcBlend) 
        /// </summary>
        ReverseSubtract,
        /// <summary>
        /// The function will extracts minimum of the source and destination. min((srcColor * srcBlend),(destColor * destBlend))
        /// </summary>
        Min, 
        /// <summary>
        /// The function will extracts maximum of the source and destination. max((srcColor * srcBlend),(destColor * destBlend))
        /// </summary>
		Max
	}
}
