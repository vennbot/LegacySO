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

namespace Mp3Sharp
{
	/// <summary>
	/// Some samples that show the use of the Mp3Stream class.
	/// </summary>
	internal class Sample
	{
		public static readonly string Mp3FilePath = @"c:\sample.mp3";

		/// <summary>
		/// Sample showing how to read through an MP3 file and obtain its contents as a PCM byte stream.
		/// </summary>
		public static void ReadAllTheWayThroughMp3File()
		{
			Mp3Stream stream = new Mp3Stream(Mp3FilePath);

			// Create the buffer
			int numberOfPcmBytesToReadPerChunk = 512;
			byte[] buffer = new byte[numberOfPcmBytesToReadPerChunk];

			int bytesReturned = -1;
			int totalBytes = 0;
			while (bytesReturned != 0)
			{
				bytesReturned = stream.Read(buffer, 0, buffer.Length);
				totalBytes += bytesReturned;
			}
			Console.WriteLine("Read a total of " + totalBytes + " bytes.");
		}

	}
}
