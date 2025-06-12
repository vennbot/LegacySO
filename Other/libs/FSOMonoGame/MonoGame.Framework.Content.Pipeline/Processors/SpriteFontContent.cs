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
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace Microsoft.Xna.Framework.Content.Pipeline.Graphics
{
	public class SpriteFontContent
    {
        public SpriteFontContent() { }

        public SpriteFontContent(FontDescription desc)
        {
            FontName = desc.FontName;
            Style = desc.Style;
            FontSize = desc.Size;
            CharacterMap = new List<char>(desc.Characters.Count);
            VerticalLineSpacing = (int)desc.Spacing; // Will be replaced in the pipeline.
            HorizontalSpacing = desc.Spacing;

            DefaultCharacter = desc.DefaultCharacter;
        }

        public string FontName = string.Empty;

        FontDescriptionStyle Style = FontDescriptionStyle.Regular;

        public float FontSize;

        public Texture2DContent Texture = new Texture2DContent();

        public List<Rectangle> Glyphs = new List<Rectangle>();

        public List<Rectangle> Cropping = new List<Rectangle>();

        public List<Char> CharacterMap = new List<Char>();

        public int VerticalLineSpacing;

        public float HorizontalSpacing;

        public List<Vector3> Kerning = new List<Vector3>();

        public Nullable<Char> DefaultCharacter;	 

    }
}
