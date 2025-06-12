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
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace MSDFData
{
    public class FieldFont
    {
        [ContentSerializer] private readonly Dictionary<char, FieldGlyph> Glyphs;
        [ContentSerializer] private readonly string NameBackend;
        [ContentSerializer] private readonly float PxRangeBackend;
        [ContentSerializer] private readonly List<KerningPair> KerningPairsBackend;
        [ContentSerializer] private readonly FieldAtlas AtlasBackend;

        public FieldFont()
        {
        }

        public FieldFont(string name, IReadOnlyCollection<FieldGlyph> glyphs, IReadOnlyCollection<KerningPair> kerningPairs, float pxRange, FieldAtlas atlas)
        {
            this.NameBackend = name;
            this.PxRangeBackend = pxRange;
            this.KerningPairsBackend = kerningPairs.ToList();
            this.AtlasBackend = atlas;

            this.Glyphs = new Dictionary<char, FieldGlyph>(glyphs.Count);
            foreach (var glyph in glyphs)
            {
                this.Glyphs.Add(glyph.Character, glyph);
            }
            FieldGlyph space;
            if (this.Glyphs.TryGetValue(' ', out space))
            {
                this.Glyphs['\u00A0'] = space;
            }
        }

        /// <summary>
        /// Name of the font
        /// </summary>
        public string Name => this.NameBackend;

        /// <summary>
        /// Distance field effect range in pixels
        /// </summary>
        public float PxRange => this.PxRangeBackend;

        /// <summary>
        /// Kerning pairs available in this font
        /// </summary>
        public IReadOnlyList<KerningPair> KerningPairs => this.KerningPairsBackend;

        /// <summary>
        /// Characters supported by this font
        /// </summary>
        [ContentSerializerIgnore]
        public IEnumerable<char> SupportedCharacters => this.Glyphs.Keys;

        private Dictionary<int, KerningPair> StringToPairBackend;
        [ContentSerializerIgnore]
        public Dictionary<int, KerningPair> StringToPair {
            get
            {
                if (StringToPairBackend == null)
                {
                    StringToPairBackend = KerningPairs.ToDictionary(x => x.Left | (x.Right << 16));
                }

                return StringToPairBackend;
            }
        }

        /// <summary>
        /// Characters supported by this font
        /// </summary>
        public FieldAtlas Atlas => AtlasBackend;
       
        /// <summary>
        /// Returns the glyph for the given character, or returns null when the glyph is not supported by this font
        /// </summary>        
        public FieldGlyph GetGlyph(char c)
        {
            if (this.Glyphs.TryGetValue(c, out FieldGlyph glyph))
            {
                return glyph;
            }

            return null;
        }
    }
}
