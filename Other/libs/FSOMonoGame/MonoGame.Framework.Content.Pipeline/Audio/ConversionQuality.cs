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

namespace Microsoft.Xna.Framework.Content.Pipeline.Audio
{
    /// <summary>
    /// Compression quality of the audio content.
    /// </summary>
    public enum ConversionQuality
    {
        /// <summary>
        /// High compression yielding lower file size, but could compromise audio quality
        /// </summary>
        Low,

        /// <summary>
        /// Moderate compression resulting in a compromise between audio quality and file size
        /// </summary>
        Medium,

        /// <summary>
        /// Lowest compression, but the best audio quality
        /// </summary>
        Best,
    }
}
