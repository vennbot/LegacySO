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
    class VertexBufferReader : ContentTypeReader<VertexBuffer>
    {
        protected internal override VertexBuffer Read(ContentReader input, VertexBuffer existingInstance)
        {
            var declaration = input.ReadRawObject<VertexDeclaration>();
            var vertexCount = (int)input.ReadUInt32();
            int dataSize = vertexCount * declaration.VertexStride;
            byte[] data = input.ContentManager.GetScratchBuffer(dataSize);
            input.Read(data, 0, dataSize);

            var buffer = new VertexBuffer(input.GraphicsDevice, declaration, vertexCount, BufferUsage.None);
            buffer.SetData(data, 0, dataSize);
            return buffer;
        }
    }
}
