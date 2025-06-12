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

using Microsoft.Xna.Framework.Graphics;
namespace Microsoft.Xna.Framework.Content
{
    internal class VertexDeclarationReader : ContentTypeReader<VertexDeclaration>
	{
		protected internal override VertexDeclaration Read(ContentReader reader, VertexDeclaration existingInstance)
        {
			var vertexStride = reader.ReadInt32();
			var elementCount = reader.ReadInt32();
			VertexElement[] elements = new VertexElement[elementCount];
			for (int i = 0; i < elementCount; ++i)
			{
				var offset = reader.ReadInt32();
				var elementFormat = (VertexElementFormat)reader.ReadInt32();
				var elementUsage = (VertexElementUsage)reader.ReadInt32();
				var usageIndex = reader.ReadInt32();
				elements[i] = new VertexElement(offset, elementFormat, elementUsage, usageIndex);
			}

            return VertexDeclaration.GetOrCreate(vertexStride, elements);
		}
	}
}
