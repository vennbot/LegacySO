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

namespace Microsoft.Xna.Framework.Content.Pipeline.Audio
{
    /// <summary>
    /// Helper methods for audio importing, conversion and processing.
    /// </summary>
    class AudioHelper
    {
        // This array must remain in sync with the ConversionFormat enum.
        static string[] conversionFormatExtensions = new[] { "wav", "wav", "wma", "xma", "wav", "m4a", "ogg" };

        /// <summary>
        /// Gets the file extension for an audio format.
        /// </summary>
        /// <param name="format">The conversion format</param>
        /// <returns>The file extension for the given conversion format.</returns>
        static public string GetExtension(ConversionFormat format)
        {
            return conversionFormatExtensions[(int)format];
        }
    }
}
