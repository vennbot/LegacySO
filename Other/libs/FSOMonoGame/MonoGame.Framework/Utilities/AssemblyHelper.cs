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
using System.Reflection;

namespace MonoGame.Utilities
{
    internal static class AssemblyHelper
    {
        public static string GetDefaultWindowTitle()
        {
            // Set the window title.
            string windowTitle = string.Empty;

            // When running unit tests this can return null.
            var assembly = Assembly.GetEntryAssembly();
            if (assembly != null)
            {
                // Use the Title attribute of the Assembly if possible.
                try
                {
                    var assemblyTitleAtt = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTitleAttribute)));
                    if (assemblyTitleAtt != null)
                        windowTitle = assemblyTitleAtt.Title;
                }
                catch
                {
                    // Nope, wasn't possible :/
                }

                // Otherwise, fallback to the Name of the assembly.
                if (string.IsNullOrEmpty(windowTitle))
                    windowTitle = assembly.GetName().Name;
            }

            return windowTitle;
        }
    }
}
