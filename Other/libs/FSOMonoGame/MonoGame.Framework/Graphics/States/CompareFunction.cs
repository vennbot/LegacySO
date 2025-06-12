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
    /// The comparison function used for depth, stencil, and alpha tests.
    /// </summary>
    public enum CompareFunction
    {
        /// <summary>
        /// Always passes the test.
        /// </summary>
        Always,
        /// <summary>
        /// Never passes the test.
        /// </summary>
        Never,
        /// <summary>
        /// Passes the test when the new pixel value is less than current pixel value.
        /// </summary>
        Less,
        /// <summary>
        /// Passes the test when the new pixel value is less than or equal to current pixel value.
        /// </summary>
        LessEqual,
        /// <summary>
        /// Passes the test when the new pixel value is equal to current pixel value.
        /// </summary>
        Equal,
        /// <summary>
        /// Passes the test when the new pixel value is greater than or equal to current pixel value.
        /// </summary>
        GreaterEqual,
        /// <summary>
        /// Passes the test when the new pixel value is greater than current pixel value.
        /// </summary>
        Greater,
        /// <summary>
        /// Passes the test when the new pixel value does not equal to current pixel value.
        /// </summary>
        NotEqual
    }
}
