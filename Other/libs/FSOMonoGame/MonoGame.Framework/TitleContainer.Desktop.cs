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
using MonoGame.Utilities;

namespace Microsoft.Xna.Framework
{
    partial class TitleContainer
    {
        static partial void PlatformInit()
        {
#if WINDOWS || DESKTOPGL
#if DESKTOPGL
            // Check for the package Resources Folder first. This is where the assets
            // will be bundled.
            if (CurrentPlatform.OS == OS.MacOSX)
                Location = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "..", "Resources");
            if (!Directory.Exists (Location))
#endif
            Location = AppDomain.CurrentDomain.BaseDirectory;
#endif
        }

        private static Stream PlatformOpenStream(string safeName)
        {
            var absolutePath = Path.Combine(Location, safeName);
            return File.OpenRead(absolutePath);
        }
    }
}

