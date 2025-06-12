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
using Microsoft.DirectX.DirectSound;

namespace Mp3Sharp
{

	/// <summary>
	/// Utility functions for working with sound.
	/// </summary>
	public class SoundUtil
	{
		private SoundUtil() { }

		/// <summary>
		/// Helper method for creating WaveFormat instances
		/// </summary>
		/// <param name="samplingRate">Sampling rate</param>
		/// <param name="bitsPerSample">Bits per sample</param>
		/// <param name="numChannels">Channels</param>
		/// <returns></returns>
		public static WaveFormat CreateWaveFormat(int samplingRate, short bitsPerSample, short numChannels)
		{
			WaveFormat wf = new WaveFormat();

			wf.FormatTag = WaveFormatTag.Pcm;
			wf.SamplesPerSecond = samplingRate;
			wf.BitsPerSample = bitsPerSample;
			wf.Channels = numChannels;

			wf.BlockAlign = (short)(wf.Channels * (wf.BitsPerSample / 8));
			wf.AverageBytesPerSecond = wf.SamplesPerSecond * wf.BlockAlign;

			return wf;
		}

	}
}
