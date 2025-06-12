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
using Microsoft.Xna.Framework.Content;

namespace MSDFData
{    
    public class FieldGlyph
    {        
        [ContentSerializer] private readonly char CharacterBackend;
        [ContentSerializer] private readonly int AtlasIndexBackend;
        [ContentSerializer] private readonly Metrics MetricsBackend;

        public FieldGlyph()
        {
           
        }

        public FieldGlyph(char character, int atlasIndex, Metrics metrics)
        {
            this.CharacterBackend = character;
            this.AtlasIndexBackend = atlasIndex;
            this.MetricsBackend = metrics;
        }
        
        /// <summary>
        /// The character this glyph represents
        /// </summary>
        public char Character => this.CharacterBackend;
        /// <summary>
        /// Index of this character in the atlas.
        /// </summary>
        public int AtlasIndex => this.AtlasIndexBackend;                
        /// <summary>
        /// Metrics for this character
        /// </summary>
        public Metrics Metrics => this.MetricsBackend;
    }
}
