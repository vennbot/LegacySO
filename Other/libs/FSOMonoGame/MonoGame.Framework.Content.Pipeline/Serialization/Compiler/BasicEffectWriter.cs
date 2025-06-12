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

using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler
{
    [ContentTypeWriter]
    class BasicEffectWriter : BuiltInContentWriter<BasicMaterialContent>
    {
        protected internal override void Write(ContentWriter output, BasicMaterialContent value)
        {
            output.WriteExternalReference(value.Textures.ContainsKey(BasicMaterialContent.TextureKey) ? value.Texture : null);
            output.Write(value.DiffuseColor ?? new Vector3(1, 1, 1));
            output.Write(value.EmissiveColor ?? new Vector3(0, 0, 0));
            output.Write(value.SpecularColor ?? new Vector3(1, 1, 1));
            output.Write(value.SpecularPower ?? 16);
            output.Write(value.Alpha ?? 1);
            output.Write(value.VertexColorEnabled ?? false);
        }
    }
}
