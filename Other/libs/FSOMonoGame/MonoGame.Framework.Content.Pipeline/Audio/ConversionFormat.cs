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
    /// Target formats supported for audio source conversions.
    /// </summary>
    public enum ConversionFormat
    {
        /// <summary>
        /// Microsoft ADPCM encoding technique using 4 bits
        /// </summary>
        Adpcm,

        /// <summary>
        /// 8/16-bit mono/stereo PCM audio 8KHz-48KHz
        /// </summary>
        Pcm,

        /// <summary>
        /// Windows Media CBR formats (64 kbps, 128 kbps, 192 kbps)
        /// </summary>
        WindowsMedia,

        /// <summary>
        /// The Xbox compression format
        /// </summary>
        Xma,

        /// <summary>
        /// QuickTime ADPCM format
        /// </summary>
        ImaAdpcm,

        /// <summary>
        /// Advanced Audio Coding
        /// </summary>
        Aac,

        /// <summary>
        /// Vorbis open, patent-free audio encoding
        /// </summary>
        Vorbis,
    }
}
