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
using System;
using System.IO;

namespace MonoGame.Tools.Pipeline
{
    public static class Util
    {
        /// <summary>        
        /// Returns the path 'filspec' made relative path 'folder'.
        /// 
        /// If 'folder' is not an absolute path, throws ArgumentException.
        /// If 'filespec' is not an absolute path, returns 'filespec' unmodified.
        /// </summary>
        public static string GetRelativePath(string filespec, string folder)
        {
            if (!Path.IsPathRooted(filespec))
                return filespec;

            if (!Path.IsPathRooted(folder))
                throw new ArgumentException("Must be an absolute path.", "folder");

            var pathUri = new Uri(filespec);

            if (folder[folder.Length-1] != Path.DirectorySeparatorChar)
                folder += Path.DirectorySeparatorChar;

            var folderUri = new Uri(folder);
            var result = folderUri.MakeRelativeUri(pathUri).ToString();
            result = result.Replace('/', Path.DirectorySeparatorChar);
            result = Uri.UnescapeDataString(result);

            return result;
        }
    }
}
