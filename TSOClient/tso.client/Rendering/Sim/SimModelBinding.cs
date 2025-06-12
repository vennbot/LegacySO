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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimsLib.ThreeD;
using Microsoft.Xna.Framework.Graphics;
using TSOClient.Code.Data;

namespace TSOClient.Code.Rendering.Sim
{
    /// <summary>
    /// Sims are made of body parts, each body part is a binding.
    /// A binding is made up of a mesh & a texture.
    /// </summary>
    public class SimModelBinding
    {
        public SimModelBinding(ulong bindingID)
        {
            BindingID = bindingID;
            
            var binding = SimCatalog.GetBinding(bindingID);
            Mesh = SimCatalog.GetOutfitMesh(binding.MeshAssetID);
            Texture = SimCatalog.GetOutfitTexture(binding.TextureAssetID);
        }

        public ulong BindingID;
        public Mesh Mesh;
        public Texture2D Texture;
    }
}
