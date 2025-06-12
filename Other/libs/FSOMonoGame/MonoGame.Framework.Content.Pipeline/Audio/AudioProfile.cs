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
using System.Collections.Generic;
using System.Linq;


namespace Microsoft.Xna.Framework.Content.Pipeline.Audio
{
    public abstract class AudioProfile
    {
        private static readonly LoadedTypeCollection<AudioProfile> _profiles = new LoadedTypeCollection<AudioProfile>();

        /// <summary>
        /// Find the profile for this target platform.
        /// </summary>
        /// <param name="platform">The platform target for audio.</param>
        /// <returns></returns>
        public static AudioProfile ForPlatform(TargetPlatform platform)
        {
            var profile = _profiles.FirstOrDefault(h => h.Supports(platform));
            if (profile != null)
                return profile;

            throw new PipelineException("There is no supported audio profile for the '" + platform + "' platform!");
        }

        /// <summary>
        /// Returns true if this profile supports audio processing for this platform.
        /// </summary>
        public abstract bool Supports(TargetPlatform platform);

        /// <summary>
        /// Converts the audio content to work on targeted platform.
        /// </summary>
        /// <param name="platform">The platform to build the audio content for.</param>
        /// <param name="quality">The suggested audio quality level.</param>
        /// <param name="content">The audio content to convert.</param>
        /// <returns>The quality used for conversion which could be different from the suggested quality.</returns>
        public abstract ConversionQuality ConvertAudio(TargetPlatform platform, ConversionQuality quality, AudioContent content);

        /// <summary>
        /// Converts the audio content to a streaming format that works on targeted platform.
        /// </summary>
        /// <param name="platform">The platform to build the audio content for.</param>
        /// <param name="quality">The suggested audio quality level.</param>
        /// <param name="content">he audio content to convert.</param>
        /// <param name="outputFileName"></param>
        /// <returns>The quality used for conversion which could be different from the suggested quality.</returns>
        public abstract ConversionQuality ConvertStreamingAudio(TargetPlatform platform, ConversionQuality quality, AudioContent content, ref string outputFileName);


        protected static int QualityToSampleRate(ConversionQuality quality, int sourceSampleRate)
        {
            switch (quality)
            {
                case ConversionQuality.Low:
                    return Math.Max(8000, (int)Math.Floor(sourceSampleRate / 2.0));
                case ConversionQuality.Medium:
                    return Math.Max(8000, (int)Math.Floor((sourceSampleRate / 4.0) * 3));
            }

            return Math.Max(8000, sourceSampleRate);
        }

        protected static int QualityToBitRate(ConversionQuality quality)
        {
            switch (quality)
            {
                case ConversionQuality.Low:
                    return 96000;
                case ConversionQuality.Medium:
                    return 128000;
            }

            return 192000;
        }
    }
}
