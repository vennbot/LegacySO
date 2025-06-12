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
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler
{
    [ContentTypeWriter]
    class Texture2DWriter : BuiltInContentWriter<Texture2DContent>
    {
        protected internal override void Write(ContentWriter output, Texture2DContent value)
        {
            var mipmaps = value.Faces[0];   // Mipmap chain.
            var level0 = mipmaps[0];        // Most detailed mipmap level.

            SurfaceFormat format;
            if (!level0.TryGetFormat(out format))
                throw new Exception("Couldn't get Format for TextureContent.");

            output.Write((int)format);
            output.Write(level0.Width);
            output.Write(level0.Height);
            output.Write(mipmaps.Count);    // Number of mipmap levels.

            foreach (var level in mipmaps)
            {
                var pixelData = level.GetPixelData();
                output.Write(pixelData.Length);
                output.Write(pixelData);
            }
        }
    }
}
