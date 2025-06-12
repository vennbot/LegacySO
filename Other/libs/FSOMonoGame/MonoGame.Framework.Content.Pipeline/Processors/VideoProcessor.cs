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

using MonoGame.Framework.Content.Pipeline.Builder;
using System.IO;

namespace Microsoft.Xna.Framework.Content.Pipeline.Processors
{
    [ContentProcessor(DisplayName = "Video - MonoGame")]
    public class VideoProcessor : ContentProcessor<VideoContent, VideoContent>
    {
        public override VideoContent Process(VideoContent input, ContentProcessorContext context)
        {
            var relative = Path.GetDirectoryName(PathHelper.GetRelativePath(context.OutputDirectory, context.OutputFilename));
            var relVideoPath = PathHelper.Normalize(Path.Combine(relative, Path.GetFileName(input.Filename)));
            var absVideoPath = PathHelper.Normalize(Path.Combine(context.OutputDirectory, relVideoPath));

            // Make sure the output folder for the video exists.
            Directory.CreateDirectory(Path.GetDirectoryName(absVideoPath));

            // Copy the already encoded video file over
            File.Copy(input.Filename, absVideoPath, true);
            context.AddOutputFile(absVideoPath);

            // Fixup relative path
            input.Filename = PathHelper.GetRelativePath(context.OutputFilename, absVideoPath);

            return input;
        }
    }
}
