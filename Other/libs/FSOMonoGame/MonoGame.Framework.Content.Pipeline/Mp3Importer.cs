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
using Microsoft.Xna.Framework.Content.Pipeline.Audio;

namespace Microsoft.Xna.Framework.Content.Pipeline
{
    /// <summary>
    /// Provides methods for reading MP3 audio files for use in the Content Pipeline.
    /// </summary>
    [ContentImporter(".mp3", DisplayName = "Mp3 Importer - MonoGame", DefaultProcessor = "SongProcessor")]
    public class Mp3Importer : ContentImporter<AudioContent>
    {
        /// <summary>
        /// Initializes a new instance of Mp3Importer.
        /// </summary>
        public Mp3Importer()
        {
        }

        /// <summary>
        /// Called by the XNA Framework when importing an MP3 audio file to be used as a game asset. This is the method called by the XNA Framework when an asset is to be imported into an object that can be recognized by the Content Pipeline.
        /// </summary>
        /// <param name="filename">Name of a game asset file.</param>
        /// <param name="context">Contains information for importing a game asset, such as a logger interface.</param>
        /// <returns>Resulting game asset.</returns>
        public override AudioContent Import(string filename, ContentImporterContext context)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("filename");
            if (context == null)
                throw new ArgumentNullException("context");

            if (!File.Exists(filename))
                throw new FileNotFoundException(string.Format("Could not locate audio file {0}.", Path.GetFileName(filename)));

            var content = new AudioContent(filename, AudioFileType.Mp3);
            return content;
        }
    }
}
