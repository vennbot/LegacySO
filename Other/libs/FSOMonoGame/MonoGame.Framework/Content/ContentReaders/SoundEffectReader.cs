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
using Microsoft.Xna.Framework.Audio;


namespace Microsoft.Xna.Framework.Content
{
	internal class SoundEffectReader : ContentTypeReader<SoundEffect>
	{
		protected internal override SoundEffect Read(ContentReader input, SoundEffect existingInstance)
		{         
            // XNB format for SoundEffect...
            //            
            // Byte [format size]	Format	WAVEFORMATEX structure
            // UInt32	Data size	
            // Byte [data size]	Data	Audio waveform data
            // Int32	Loop start	In bytes (start must be format block aligned)
            // Int32	Loop length	In bytes (length must be format block aligned)
            // Int32	Duration	In milliseconds

            // The header containss the WAVEFORMATEX header structure
            // defined as the following...
            //
            //  WORD  wFormatTag;       // byte[0]  +2
            //  WORD  nChannels;        // byte[2]  +2
            //  DWORD nSamplesPerSec;   // byte[4]  +4
            //  DWORD nAvgBytesPerSec;  // byte[8]  +4
            //  WORD  nBlockAlign;      // byte[12] +2
            //  WORD  wBitsPerSample;   // byte[14] +2
            //  WORD  cbSize;           // byte[16] +2
            //
            // We let the sound effect deal with parsing this based
            // on what format the audio data actually is.

		    var headerSize = input.ReadInt32();
            var header = input.ReadBytes(headerSize);

            // Read the audio data buffer.
            var dataSize = input.ReadInt32();
            var data = input.ContentManager.GetScratchBuffer(dataSize);
            input.Read(data, 0, dataSize);

            var loopStart = input.ReadInt32();
            var loopLength = input.ReadInt32();
            var durationMs = input.ReadInt32();

            // Create the effect.
            var effect = new SoundEffect(header, data, dataSize, durationMs, loopStart, loopLength);

            // Store the original asset name for debugging later.
            effect.Name = input.AssetName;

            return effect;
        }
	}
}
