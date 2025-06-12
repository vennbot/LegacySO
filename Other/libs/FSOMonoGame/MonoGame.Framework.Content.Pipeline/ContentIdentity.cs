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

namespace Microsoft.Xna.Framework.Content.Pipeline
{
    /// <summary>
    /// Provides properties describing the origin of the game asset, such as the original source file and creation tool. This information is used for error reporting, and by processors that need to determine from what directory the asset was originally loaded.
    /// </summary>
    [Serializable]
    public class ContentIdentity
    {
        /// <summary>
        /// Gets or sets the specific location of the content item within the larger source file.
        /// </summary>
        public string FragmentIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the file name of the asset source.
        /// </summary>
        public string SourceFilename { get; set; }

        /// <summary>
        /// Gets or sets the creation tool of the asset.
        /// </summary>
        public string SourceTool { get; set; }

        /// <summary>
        /// Initializes a new instance of ContentIdentity.
        /// </summary>
        public ContentIdentity()
            : this(string.Empty, string.Empty, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of ContentIdentity with the specified values.
        /// </summary>
        /// <param name="sourceFilename">The absolute path to the file name of the asset source.</param>
        public ContentIdentity(string sourceFilename)
            : this(sourceFilename, string.Empty, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of ContentIdentity with the specified values.
        /// </summary>
        /// <param name="sourceFilename">The absolute path to the file name of the asset source.</param>
        /// <param name="sourceTool">The name of the digital content creation (DCC) tool that created the asset.</param>
        public ContentIdentity(string sourceFilename, string sourceTool)
            : this(sourceFilename, sourceTool, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of ContentIdentity with the specified values.
        /// </summary>
        /// <param name="sourceFilename">The absolute path to the file name of the asset source.</param>
        /// <param name="sourceTool">The name of the digital content creation (DCC) tool that created the asset.</param>
        /// <param name="fragmentIdentifier">Specific location of the content item within the larger source file. For example, this could be a line number in the file.</param>
        public ContentIdentity(string sourceFilename, string sourceTool, string fragmentIdentifier)
        {
            SourceFilename = sourceFilename;
            SourceTool = sourceTool;
            FragmentIdentifier = fragmentIdentifier;
        }
    }
}
