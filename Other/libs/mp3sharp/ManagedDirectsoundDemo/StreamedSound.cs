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
using System.IO;

using Microsoft.DirectX.DirectSound;

namespace Mp3Sharp
{

	/// <summary>
	/// Plays streamed PCM-format sounds.
	/// </summary>
	public class StreamedSound 
	{
		public StreamedSound(Device device, Stream stream, WaveFormat waveFormat)
		{
			Device = device; Stream = stream; WaveFormat = waveFormat;
		}

		private Device Device;

		/// <summary>
		/// Gets or sets the source stream used to provide PCM-encoded bytes.
		/// </summary>
		public Stream Stream
		{
			get { return StreamRep; }
			set { StreamRep = value; }
		}
		private Stream StreamRep = null;

		private void InitBuffer()
		{
			OnBufferInitializing();
			if (ERSB == null)
			{
				ERSB = new EventRaisingSoundBuffer(Device, WaveFormat, BufferLength);
				ERSB.BufferNotification += new BufferNotificationEventHandler(OnBufferNotification);
			}
			else
			{
				ERSB.WaveFormat = WaveFormat;
				ERSB.BufferLength = BufferLength;
			}
			OnBufferInitialized();
		}

		protected virtual void OnBufferInitializing() {}
		protected virtual void OnBufferInitialized() {}

		public WaveFormat WaveFormat
		{
			get { return WaveFormatRep; }
			set { WaveFormatRep = value; if (ERSB != null) ERSB.WaveFormat = value; }
		}
		public WaveFormat WaveFormatRep;

		public TimeSpan BufferLength
		{
			get { return BufferLengthRep; }
			set { BufferLengthRep = value; if (ERSB != null) ERSB.BufferLength = value; }
		}
		private TimeSpan BufferLengthRep = TimeSpan.FromSeconds(1);

		/// <summary>
		/// The event-raising sound buffer used by the streamed sound.
		/// </summary>
		protected EventRaisingSoundBuffer ERSB;

		public bool Playing 
		{ 
			get { return ERSB.Playing; } 
			set 
			{ 
				if (value) Play(); else Stop();
			}
		}

		public bool Looping 
		{ 
			get { return ERSB.Playing; } 
			set
			{ 
				if (value) Loop(); else Stop();
			}
		}



		public void OnBufferNotification(object sender, BufferNotificationEventArgs e)
		{
			if (e.NewSoundByte == null || e.NewSoundByte.Length != e.NumBytesRequired)
				e.NewSoundByte = new byte[e.NumBytesRequired];

			int bytesRead = Stream.Read(e.NewSoundByte, 0, e.NumBytesRequired);
			if (bytesRead != e.NumBytesRequired)
			{
				byte[] trimmedBytes = new byte[bytesRead];
				Array.Copy(e.NewSoundByte, trimmedBytes, bytesRead);
				e.NewSoundByte = trimmedBytes;
			}
			e.SoundFinished = Stream.Length == Stream.Position;

			if (BufferNotification != null) BufferNotification(sender, e);
		}

		/// <summary>
		/// Event that is raised after bytes are added to the buffer.  The event arguments will contain 
		/// any new bytes being provided by the streamed sound.
		/// </summary>
		public event BufferNotificationEventHandler BufferNotification;

		

		public void Play()
		{
			InitBuffer();
			ERSB.Play();
		}

		public void Loop()
		{
			InitBuffer();
			ERSB.Play();
		}

		public void Stop()
		{
			ERSB.Stop();
		}

		public void Rewind()
		{
			if (Playing) Stop();
			Stream.Position = 0;
		}
	}
}
