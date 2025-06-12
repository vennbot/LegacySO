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
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
    internal class TextureCubeReader : ContentTypeReader<TextureCube>
    {

        protected internal override TextureCube Read(ContentReader reader, TextureCube existingInstance)
        {
            TextureCube textureCube = null;

			SurfaceFormat surfaceFormat = (SurfaceFormat)reader.ReadInt32();
			int size = reader.ReadInt32();
			int levels = reader.ReadInt32();

            if (existingInstance == null)
                textureCube = new TextureCube(reader.GraphicsDevice, size, levels > 1, surfaceFormat);
            else
                textureCube = existingInstance;

#if OPENGL
            Threading.BlockOnUIThread(() =>
            {
#endif
                for (int face = 0; face < 6; face++)
                {
                    for (int i = 0; i < levels; i++)
                    {
                        int faceSize = reader.ReadInt32();
                        byte[] faceData = reader.ContentManager.GetScratchBuffer(faceSize);
                        reader.Read(faceData, 0, faceSize);
                        textureCube.SetData<byte>((CubeMapFace)face, i, null, faceData, 0, faceSize);
                    }
                }
#if OPENGL
            });
#endif

             return textureCube;
        }
    }
}
