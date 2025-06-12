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
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FSO.Client.Rendering.City
{
    /// <summary>
    /// Represents a MeshVertex that makes up a face.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TLayerVertex : IVertexType
    {
        public Vector3 Position;
        /** UV Mapping **/
        public Vector2 TextureCoord; //in shader these are xy and zw in TC0 respectively.
        public Vector2 MaskTextureCoord;
        public Vector3 Normal;
        public float Transparency; //0 is opaque.

        public static int SizeInBytes = sizeof(float) * 11;

        public static readonly VertexDeclaration VertexElements = new VertexDeclaration(

            new VertexElement(0, VertexElementFormat.Vector3,
                VertexElementUsage.Position, 0),
            new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector4,
                VertexElementUsage.TextureCoordinate, 0),
            new VertexElement(sizeof(float) * (3 + 4), VertexElementFormat.Vector4,
                VertexElementUsage.TextureCoordinate, 1)
        );

        VertexDeclaration IVertexType.VertexDeclaration
        {
            get { return VertexElements; }
        }
    }
}
