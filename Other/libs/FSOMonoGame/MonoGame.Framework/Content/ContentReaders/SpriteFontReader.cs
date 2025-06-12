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

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
    internal class SpriteFontReader : ContentTypeReader<SpriteFont>
    {
        internal SpriteFontReader()
        {
        }

        protected internal override SpriteFont Read(ContentReader input, SpriteFont existingInstance)
        {
            if (existingInstance != null)
            {
                // Read the texture into the existing texture instance
                input.ReadObject<Texture2D>(existingInstance.Texture);
                
                // discard the rest of the SpriteFont data as we are only reloading GPU resources for now
                input.ReadObject<List<Rectangle>>();
                input.ReadObject<List<Rectangle>>();
                input.ReadObject<List<char>>();
                input.ReadInt32();
                input.ReadSingle();
                input.ReadObject<List<Vector3>>();
                if (input.ReadBoolean())
                {
                    input.ReadChar();
                }

                return existingInstance;
            }
            else
            {
                // Create a fresh SpriteFont instance
                Texture2D texture = input.ReadObject<Texture2D>();
                List<Rectangle> glyphs = input.ReadObject<List<Rectangle>>();
                List<Rectangle> cropping = input.ReadObject<List<Rectangle>>();
                List<char> charMap = input.ReadObject<List<char>>();
                int lineSpacing = input.ReadInt32();
                float spacing = input.ReadSingle();
                List<Vector3> kerning = input.ReadObject<List<Vector3>>();
                char? defaultCharacter = null;
                if (input.ReadBoolean())
                {
                    defaultCharacter = new char?(input.ReadChar());
                }
                return new SpriteFont(texture, glyphs, cropping, charMap, lineSpacing, spacing, kerning, defaultCharacter);
            }
        }
    }
}
