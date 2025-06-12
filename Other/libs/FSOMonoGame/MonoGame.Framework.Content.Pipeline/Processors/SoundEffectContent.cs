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

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Xna.Framework.Content.Pipeline.Processors
{
    /// <summary>
    /// Represents a processed sound effect.
    /// </summary>
    public sealed class SoundEffectContent
    {
        internal byte[] format;
        internal byte[] data;
        internal int loopStart;
        internal int loopLength;
        internal int duration;

        /// <summary>
        /// Initializes a new instance of the SoundEffectContent class.
        /// </summary>
        /// <param name="format">The WAV header.</param>
        /// <param name="data">The audio waveform data.</param>
        /// <param name="loopStart">The start of the loop segment (must be block aligned).</param>
        /// <param name="loopLength">The length of the loop segment (must be block aligned).</param>
        /// <param name="duration">The duration of the wave file in milliseconds.</param>
        internal SoundEffectContent(IEnumerable<byte> format, IEnumerable<byte> data, int loopStart, int loopLength, int duration)
        {
            this.format = format.ToArray();
            this.data = data.ToArray();
            this.loopStart = loopStart;
            this.loopLength = loopLength;
            this.duration = duration;
        }
    }
}
