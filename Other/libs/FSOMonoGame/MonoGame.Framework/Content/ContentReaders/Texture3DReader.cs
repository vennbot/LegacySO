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
    internal class Texture3DReader : ContentTypeReader<Texture3D>
    {
        protected internal override Texture3D Read(ContentReader reader, Texture3D existingInstance)
        {
            Texture3D texture = null;

            SurfaceFormat format = (SurfaceFormat)reader.ReadInt32();
            int width = reader.ReadInt32();
            int height = reader.ReadInt32();
            int depth = reader.ReadInt32();
            int levelCount = reader.ReadInt32();

            if (existingInstance == null)
                texture = new Texture3D(reader.GraphicsDevice, width, height, depth, levelCount > 1, format);
            else
                texture = existingInstance;

#if OPENGL
            Threading.BlockOnUIThread(() =>
            {
#endif
                for (int i = 0; i < levelCount; i++)
                {
                    int dataSize = reader.ReadInt32();
                    byte[] data = reader.ContentManager.GetScratchBuffer(dataSize);
                    reader.Read(data, 0, dataSize);
                    texture.SetData(i, 0, 0, width, height, 0, depth, data, 0, dataSize);

                    // Calculate dimensions of next mip level.
                    width = Math.Max(width >> 1, 1);
                    height = Math.Max(height >> 1, 1);
                    depth = Math.Max(depth >> 1, 1);
                }
#if OPENGL
            });
#endif

            return texture;
        }
    }
}
