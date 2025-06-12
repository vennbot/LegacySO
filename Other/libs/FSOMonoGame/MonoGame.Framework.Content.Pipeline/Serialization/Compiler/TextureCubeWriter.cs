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
    internal class TextureCubeWriter : BuiltInContentWriter<TextureCubeContent>
    {
        protected internal override void Write(ContentWriter output, TextureCubeContent value)
        {
            var mipmaps0 = value.Faces[0];  // Mipmap chain of face 0 (+X).
            var level0 = mipmaps0[0];       // Most detailed mipmap level of face 0.

            SurfaceFormat format;
            if (!level0.TryGetFormat(out format))
                throw new Exception("Couldn't get format for TextureCubeContent.");

            output.Write((int)format);      // Surface format
            output.Write(level0.Width);     // Cube map size
            output.Write(mipmaps0.Count);   // Number of mipmap levels

            // The number of faces in TextureCubeContent is guaranteed to be 6.
            foreach (var mipmaps in value.Faces)
            {
                foreach (var level in mipmaps)
                {
                    byte[] pixelData = level.GetPixelData();
                    output.Write(pixelData.Length);
                    output.Write(pixelData);
                }
            }
        }
    }
}
