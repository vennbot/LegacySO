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

using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler
{
    [ContentTypeWriter]
    class VertexDeclarationWriter : BuiltInContentWriter<VertexDeclarationContent>
    {
        protected internal override void Write(ContentWriter output, VertexDeclarationContent value)
        {
            // If fpr whatever reason there isn't a vertex stride defined, it's going to
            // cause problems after reading it in, so better to fail early here.
            output.Write((uint)value.VertexStride.Value);
            output.Write((uint)value.VertexElements.Count);
            foreach (var element in value.VertexElements)
            {
                output.Write((uint)element.Offset);
                output.Write((int)element.VertexElementFormat);
                output.Write((int)element.VertexElementUsage);
                output.Write((uint)element.UsageIndex);
            }
        }
    }
}
