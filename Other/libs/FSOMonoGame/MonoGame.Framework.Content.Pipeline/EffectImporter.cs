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

using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace Microsoft.Xna.Framework.Content.Pipeline
{
    /// <summary>
    /// Provides methods for reading effect (.fx) files for use in the Content Pipeline.
    /// </summary>
    [ContentImporter(".fx", DisplayName = "Effect Importer - MonoGame", DefaultProcessor = "EffectProcessor")]
    public class EffectImporter : ContentImporter<EffectContent>
    {
        /// <summary>
        /// Initializes a new instance of EffectImporter.
        /// </summary>
        public EffectImporter()
        {
        }

        /// <summary>
        /// Called by the XNA Framework when importing an .fx file to be used as a game asset. This is the method called by the XNA Framework when an asset is to be imported into an object that can be recognized by the Content Pipeline.
        /// </summary>
        /// <param name="filename">Name of a game asset file.</param>
        /// <param name="context">Contains information for importing a game asset, such as a logger interface.</param>
        /// <returns>Resulting game asset.</returns>
        public override EffectContent Import(string filename, ContentImporterContext context)
        {
            var effect = new EffectContent();
            effect.Identity = new ContentIdentity(filename);
            using (var reader = new StreamReader(filename))
                effect.EffectCode = reader.ReadToEnd();
            return effect;
        }
    }
}
