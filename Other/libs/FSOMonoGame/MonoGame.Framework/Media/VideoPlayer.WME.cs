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
using Microsoft.Xna.Framework.Graphics;

using SharpDX.MediaFoundation;

namespace Microsoft.Xna.Framework.Media
{
    public sealed partial class VideoPlayer : IDisposable
    {
        private MediaEngine _mediaEngine;
        private Texture2D _lastFrame;

        DXGIDeviceManager _devManager;

        private void PlatformInitialize()
        {
            MediaManager.Startup();

            _devManager = new DXGIDeviceManager();
            _devManager.ResetDevice(Game.Instance.GraphicsDevice._d3dDevice);

            using (var factory = new MediaEngineClassFactory())
            using (var attributes = new MediaEngineAttributes
            {
                VideoOutputFormat = (int)SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                DxgiManager = _devManager
                
            })
            {
                _mediaEngine = new MediaEngine(factory, attributes, MediaEngineCreateFlags.None, OnMediaEngineEvent);
            }
        }

        private void OnMediaEngineEvent(MediaEngineEvent mediaEvent, long param1, int param2)
        {
            if (!_mediaEngine.HasVideo())
                return;

            switch (mediaEvent)
            {
                case MediaEngineEvent.Play:
                    _lastFrame = null;
                    break;
                
                case MediaEngineEvent.Ended:

                    if (IsLooped)
                    {
                        PlatformPlay();
                        return;
                    }   
                    
                    _state = MediaState.Stopped;
                    break;
            }
        }

        private Texture2D PlatformGetTexture()
        {
            // This will return a null texture if
            // the video hasn't started playing yet
            // or the last frame if the video is stopped
            // as per XNA's behavior.
            if (_state != MediaState.Playing)
                return _lastFrame;

            long pts;
            if (!_mediaEngine.HasVideo() || !_mediaEngine.OnVideoStreamTick(out pts))
                return _lastFrame;

            _lastFrame = new Texture2D(Game.Instance.GraphicsDevice,
                                        _currentVideo.Width,
                                        _currentVideo.Height,
                                        false,
                                        SurfaceFormat.Bgra32, 
                                        Texture2D.SurfaceType.RenderTarget);

			var region = new SharpDX.Mathematics.Interop.RawRectangle(0, 0, _currentVideo.Width, _currentVideo.Height);
            _mediaEngine.TransferVideoFrame(_lastFrame._texture, null, region, null);

            return _lastFrame;
        }

        private void PlatformGetState(ref MediaState result)
        {
        }

        private void PlatformPause()
        {
            // Calling PlatformGetTexture() manually will save the last frame
            // so we can return the same one without doing unnecessary copies
            // if GetTexture() keeps getting called while paused
            PlatformGetTexture();

            _mediaEngine.Pause();
        }

        private void PlatformResume()
        {
            _mediaEngine.Play();
        }

        private void PlatformPlay()
        {
            _mediaEngine.Source = System.IO.Path.Combine(TitleContainer.Location, _currentVideo.FileName);
            _mediaEngine.Play();
        }

        private void PlatformStop()
        {
            // Calling PlatformGetTexture() manually will save the last frame
            // so we can return the same one without doing unnecessary copies
            // if GetTexture() keeps getting called while stopped
            PlatformGetTexture();

            _mediaEngine.Pause();
            _mediaEngine.CurrentTime = 0.0;
        }

        private void PlatformSetIsLooped()
        {
            throw new NotImplementedException();
        }

        private void PlatformSetIsMuted()
        {
            throw new NotImplementedException();
        }

        private TimeSpan PlatformGetPlayPosition()
        {
            return TimeSpan.FromSeconds(_mediaEngine.CurrentTime);
        }

        private void PlatformSetVolume()
        {
            _mediaEngine.Volume = _volume;
        }

        private void PlatformDispose(bool disposing)
        {
        }
    }
}
