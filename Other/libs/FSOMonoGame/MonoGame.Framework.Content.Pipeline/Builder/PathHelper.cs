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
using Microsoft.Xna.Framework.Content.Pipeline;

namespace MonoGame.Framework.Content.Pipeline.Builder
{
    public static class PathHelper
    {
        /// <summary>
        /// The/universal/standard/directory/seperator.
        /// </summary>
        public const char DirectorySeparator = '/';

        /// <summary>
        /// Returns a path string normalized to the/universal/standard.
        /// </summary>
        public static string Normalize(string path)
        {
            return path.Replace('\\', '/');
        }

        /// <summary>
        /// Returns a directory path string normalized to the/universal/standard
        /// with a trailing seperator.
        /// </summary>
        public static string NormalizeDirectory(string path)
        {
            return path.Replace('\\', '/').TrimEnd('/') + '/';
        }

        /// <summary>
        /// Returns a path string normalized to the\Windows\standard.
        /// </summary>
        public static string NormalizeWindows(string path)
        {
            return path.Replace('/', '\\');
        }

        /// <summary>
        /// Returns a path relative to the base path.
        /// </summary>
        /// <param name="basePath">The path to make relative to.  Must end with directory seperator.</param>
        /// <param name="path">The path to be made relative to the basePath.</param>
        /// <returns>The relative path or the original string if it is not absolute or cannot be made relative.</returns>
        public static string GetRelativePath(string basePath, string path)
        {
            Uri uri;
            if (!Uri.TryCreate(path, UriKind.Absolute, out uri))
                return path;

            uri = new Uri(basePath).MakeRelativeUri(uri);
            var str = Uri.UnescapeDataString(uri.ToString());

            return Normalize(str);
        }
    }
}
