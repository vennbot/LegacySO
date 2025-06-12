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
using Microsoft.Xna.Framework.Audio;
using MonoGame.OpenAL;

namespace Microsoft.Xna.Framework.Media
{
    public sealed partial class Song : IEquatable<Song>, IDisposable
    {
        private OggStream stream;
        private float _volume = 1f;

        private void PlatformInitialize(string fileName)
        {
            stream = new OggStream(fileName, OnFinishedPlaying);
            stream.Prepare();

            _duration = stream.GetLength();
        }
        
        internal void SetEventHandler(FinishedPlayingHandler handler) { }

        internal void OnFinishedPlaying()
        {
            MediaPlayer.OnSongFinishedPlaying(null, null);
        }
		
        void PlatformDispose(bool disposing)
        {
            if (stream == null)
                return;

            stream.Dispose();
            stream = null;
        }

        internal void Play(TimeSpan? startPosition)
        {
            if (stream == null)
                return;

            stream.Play();
            if (startPosition != null)
                stream.SeekToPosition((TimeSpan)startPosition);

            _playCount++;
        }

        internal void Resume()
        {
            if (stream == null)
                return;

            stream.Resume();
        }

        internal void Pause()
        {
            if (stream == null)
                return;

            stream.Pause();
        }

        internal void Stop()
        {
            if (stream == null)
                return;

            stream.Stop();
            _playCount = 0;
        }

        internal float Volume
        {
            get
            {
                if (stream == null)
                    return 0.0f;
                return _volume; 
            }
            set
            {
                _volume = value;
                if (stream != null)
                    stream.Volume = _volume;
            }
        }

        public TimeSpan Position
        {
            get
            {
                if (stream == null)
                    return TimeSpan.FromSeconds(0.0);
                return stream.GetPosition();
            }
        }

        private Album PlatformGetAlbum()
        {
            return null;
        }

        private Artist PlatformGetArtist()
        {
            return null;
        }

        private Genre PlatformGetGenre()
        {
            return null;
        }

        private TimeSpan PlatformGetDuration()
        {
            return _duration;
        }

        private bool PlatformIsProtected()
        {
            return false;
        }

        private bool PlatformIsRated()
        {
            return false;
        }

        private string PlatformGetName()
        {
            return Path.GetFileNameWithoutExtension(_name);
        }

        private int PlatformGetPlayCount()
        {
            return _playCount;
        }

        private int PlatformGetRating()
        {
            return 0;
        }

        private int PlatformGetTrackNumber()
        {
            return 0;
        }
    }
}

