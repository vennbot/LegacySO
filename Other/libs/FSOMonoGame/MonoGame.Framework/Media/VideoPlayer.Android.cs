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
using Android.Widget;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Media
{
    public sealed partial class VideoPlayer : IDisposable
    {
        private Game _game;

        private void PlatformInitialize()
        {
            _game = Game.Instance;
        }

        private Texture2D PlatformGetTexture()
        {
            throw new NotImplementedException();
        }

        private void PlatformGetState(ref MediaState result)
        {
        }

        private void PlatformPause()
        {
            _currentVideo.Player.Pause();
        }

        private void PlatformResume()
        {
            _currentVideo.Player.Start();
        }

        private void PlatformPlay()
        {
            _currentVideo.Player.SetDisplay(((AndroidGameWindow)_game.Window).GameView.Holder);
            _currentVideo.Player.Start();
            
            AndroidGamePlatform.IsPlayingVdeo = true;
        }

        private void PlatformStop()
        {
            _currentVideo.Player.Stop();

            AndroidGamePlatform.IsPlayingVdeo = false;
            _currentVideo.Player.SetDisplay(null);
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
            throw new NotImplementedException();
        }

        private TimeSpan PlatformSetVolume()
        {
            throw new NotImplementedException();
        }

        private void PlatformDispose(bool disposing)
        {
        }
    }
}
