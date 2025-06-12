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

namespace Microsoft.Xna.Framework.Content.Pipeline.Graphics
{
    public class DualTextureMaterialContent : MaterialContent
    {
        public const string AlphaKey = "Alpha";
        public const string DiffuseColorKey = "DiffuseColor";
        public const string TextureKey = "Texture";
        public const string Texture2Key = "Texture2";
        public const string VertexColorEnabledKey = "VertexColorEnabled";

        public float? Alpha
        {
            get { return GetValueTypeProperty<float>(AlphaKey); }
            set { SetProperty(AlphaKey, value); }
        }

        public Vector3? DiffuseColor
        {
            get { return GetValueTypeProperty<Vector3>(DiffuseColorKey); }
            set { SetProperty(DiffuseColorKey, value); }
        }

        public ExternalReference<TextureContent> Texture
        {
            get { return GetTexture(TextureKey); }
            set { SetTexture(TextureKey, value); }
        }

        public ExternalReference<TextureContent> Texture2
        {
            get { return GetTexture(Texture2Key); }
            set { SetTexture(Texture2Key, value); }
        }

        public bool? VertexColorEnabled
        {
            get { return GetValueTypeProperty<bool>(VertexColorEnabledKey); }
            set { SetProperty(VertexColorEnabledKey, value); }
        }
    }
}
