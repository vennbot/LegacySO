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
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline.Audio;

namespace Microsoft.Xna.Framework.Content.Pipeline
{
    /// <summary>
    /// Provides methods for reading .wav audio files for use in the Content Pipeline.
    /// </summary>
    [ContentImporter(".wav", DisplayName = "Wav Importer - MonoGame", DefaultProcessor = "SoundEffectProcessor")]
    public class WavImporter : ContentImporter<AudioContent>
    {
        /// <summary>
        /// Called by the XNA Framework when importing a .wav audio file to be used as a game asset. This is the method called by the XNA Framework when an asset is to be imported into an object that can be recognized by the Content Pipeline.
        /// </summary>
        /// <param name="filename">Name of a game asset file.</param>
        /// <param name="context">Contains information for importing a game asset, such as a logger interface.</param>
        /// <returns>Resulting game asset.</returns>
        /// <remarks>This importer supports PCM in unsigned 8-bit, signed 16-bit, signed 24-bit, IEEE Float 32-bit, MS-ADPCM or IMA/ADPCM with sample rates from 8KHz up to 48KHz.</remarks>
        public override AudioContent Import(string filename, ContentImporterContext context)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("filename");
            if (context == null)
                throw new ArgumentNullException("context");

            if (!File.Exists(filename))
                throw new FileNotFoundException(string.Format("Could not locate audio file {0}.", Path.GetFileName(filename)));

            var content = new AudioContent(filename, AudioFileType.Wav);

            // Validate the format of the input.
            if (content.Format.SampleRate < 8000 || content.Format.SampleRate > 48000)
                throw new InvalidContentException(string.Format("Audio file {0} contains audio data with unsupported sample rate of {1}KHz. Supported sample rates are from 8KHz up to 48KHz.", Path.GetFileName(filename), content.Format.SampleRate));
            var validPcm = content.Format.Format == 1 && (content.Format.BitsPerSample == 8 || content.Format.BitsPerSample == 16 || content.Format.BitsPerSample == 24);
            var validAdpcm = (content.Format.Format == 2 || content.Format.Format == 17) && content.Format.BitsPerSample == 4;
            var validIeeeFloat = content.Format.Format == 3 && content.Format.BitsPerSample == 32;
            if (!(validPcm || validAdpcm || validIeeeFloat))
                throw new InvalidContentException(string.Format("Audio file {0} contains audio data with unsupported format of {1} and bit depth of {2}. Supported bit depths are unsigned 8-bit, signed 16-bit, signed 24-bit for PCM(1) and 32-bit for IEEE Float(3).", Path.GetFileName(filename), content.Format.Format, content.Format.BitsPerSample));
            
            return content;
        }
    }
}
