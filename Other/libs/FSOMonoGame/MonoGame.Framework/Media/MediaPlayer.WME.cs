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
using SharpDX.MediaFoundation;
using SharpDX.Multimedia;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;

namespace Microsoft.Xna.Framework.Media
{
    public static partial class MediaPlayer
    {
        // RAYB: This needs to be turned back into a readonly.
        private static MediaEngine _mediaEngineEx;
        private static CoreDispatcher _dispatcher;

        private enum SessionState { Stopped, Started, Paused }
        private static SessionState _sessionState = SessionState.Stopped;
        private static TimeSpan? _desiredPosition;

        private static void PlatformInitialize()
        {
            MediaManager.Startup(true);
            using (var factory = new MediaEngineClassFactory())
            using (var attributes = new MediaEngineAttributes { AudioCategory = AudioStreamCategory.GameMedia })
            {
                var creationFlags = MediaEngineCreateFlags.AudioOnly;

                var mediaEngine = new MediaEngine(factory, attributes, creationFlags, MediaEngineExOnPlaybackEvent);
                _mediaEngineEx = mediaEngine.QueryInterface<MediaEngineEx>();
            }


            _dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
        }

        private static void MediaEngineExOnPlaybackEvent(MediaEngineEvent mediaEvent, long param1, int param2)
        {
            if (mediaEvent == MediaEngineEvent.LoadedData)
            {
                if (_desiredPosition.HasValue)
                    _mediaEngineEx.CurrentTime = _desiredPosition.Value.TotalSeconds;
                if (_sessionState == SessionState.Started)
                    _mediaEngineEx.Play();
            }
            if (mediaEvent == MediaEngineEvent.Ended)
            {
                _sessionState = SessionState.Stopped;
                _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => OnSongFinishedPlaying(null, null)).AsTask();
            }
        }

        #region Properties

        private static bool PlatformGetIsMuted()
        {
            return _isMuted;
        }

        private static void PlatformSetIsMuted(bool muted)
        {
            _isMuted = muted;

            _mediaEngineEx.Muted = _isMuted;
        }

        private static bool PlatformGetIsRepeating()
        {
            return _isRepeating;
        }

        private static void PlatformSetIsRepeating(bool repeating)
        {
            _isRepeating = repeating;

            _mediaEngineEx.Loop = _isRepeating;
        }

        private static bool PlatformGetIsShuffled()
        {
            return _isShuffled;
        }

        private static void PlatformSetIsShuffled(bool shuffled)
        {
            _isShuffled = shuffled;
        }

        private static TimeSpan PlatformGetPlayPosition()
        {
            return TimeSpan.FromSeconds(_mediaEngineEx.CurrentTime);
        }

        private static bool PlatformGetGameHasControl()
        {
            // TODO: Fix me!
            return true;
        }

        private static MediaState PlatformGetState()
        {
            return _state;
        }

        private static float PlatformGetVolume()
        {
            return _volume;
        }

        private static void PlatformSetVolume(float volume)
        {
            _volume = volume;

            _mediaEngineEx.Volume = _volume;
        }

        #endregion

        private static void PlatformPause()
        {
            if (_sessionState != SessionState.Started)
                return;
            _sessionState = SessionState.Paused;
            _mediaEngineEx.Pause();
        }

        private static void PlatformPlaySong(Song song, TimeSpan? startPosition)
        {
            _mediaEngineEx.Source = song.FilePath;
            _mediaEngineEx.Load();
            _desiredPosition = startPosition;
            _sessionState = SessionState.Started;

            //We start playing when we get a LoadedData event in MediaEngineExOnPlaybackEvent
        }

        private static void PlatformResume()
        {
            if (_sessionState != SessionState.Paused)
                return;
            _mediaEngineEx.Play();
        }

        private static void PlatformStop()
        {
            if (_sessionState == SessionState.Stopped)
                return;
            _mediaEngineEx.Source = null;
        }
    }
}

