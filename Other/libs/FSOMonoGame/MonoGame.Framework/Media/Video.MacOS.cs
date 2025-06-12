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

using ObjCRuntime;
using Foundation;
using AVFoundation;

namespace Microsoft.Xna.Framework.Media
{
    /// <summary>
    /// Represents a video.
    /// </summary>
    public sealed partial class Video : IDisposable
    {
        AVPlayerItem movie;

        internal AVPlayer Player { get; private set; }

        internal float Volume
        {
            get { return Player.Volume; }
            set
            {
                // TODO When Xamarain fix the set Volume mMovie.Volume = value;
            }
        }

        internal TimeSpan CurrentPosition
        {
            get { return new TimeSpan(movie.CurrentTime.Value); }
        }

        private void PlatformInitialize()
        {
            var err = new NSError();

            movie = AVPlayerItem.FromUrl(NSUrl.FromFilename(FileName));
            Player = new AVPlayer(movie);
        }

        private void PlatformDispose(bool disposing)
        {
            if (Player != null)
            {
                Player.Dispose();
                Player = null;
            }

            if (movie != null)
            {
                movie.Dispose();
                movie = null;
            }
        }
    }
}
