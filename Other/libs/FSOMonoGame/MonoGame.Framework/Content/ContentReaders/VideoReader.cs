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
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Utilities;

namespace Microsoft.Xna.Framework.Content
{
    internal class VideoReader : ContentTypeReader<Video>
    {
        protected internal override Video Read(ContentReader input, Video existingInstance)
        {
            string path = input.ReadObject<string>();

            if (!string.IsNullOrEmpty(path))
            {
                // Add the ContentManager's RootDirectory
                var dirPath = Path.Combine(input.ContentManager.RootDirectoryFullPath, input.AssetName);

                // Resolve the relative path
                path = FileHelpers.ResolveRelativePath(dirPath, path);
            }

            var durationMS = input.ReadObject<int>();
            var width = input.ReadObject<int>();
            var height = input.ReadObject<int>();
            var framesPerSecond = input.ReadObject<float>();
            var soundTrackType = input.ReadObject<int>();  // 0 = Music, 1 = Dialog, 2 = Music and dialog

            return new Video(path, durationMS)
            {
                Width = width,
                Height = height,
                FramesPerSecond = framesPerSecond,
                VideoSoundtrackType = (VideoSoundtrackType)soundTrackType
            };
        }
    }
}
