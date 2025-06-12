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

using SharpDX.MediaFoundation;

namespace Microsoft.Xna.Framework.Media
{
    /// <summary>
    /// This class provides a way for the MediaManager to be initialised exactly once, 
    /// regardless of how many different places need it, and which is called first.
    /// </summary>
    internal sealed class MediaManagerState
    {
        private static bool started;

        /// <summary>
        /// Ensures that the MediaManager has been initialised. Must be called from UI thread.
        /// </summary>
        public static void CheckStartup()
        {
            if(!started)
            {
                started = true;
                MediaManager.Startup(true);
            }
        }

        /// <summary>
        /// Ensures that the MediaManager has been shutdown. Must be called from UI thread.
        /// </summary>
        public static void CheckShutdown()
        {
            if(started)
            {
                started = false;
                MediaManager.Shutdown();
            }
        }
    }
}
