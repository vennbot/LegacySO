
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

namespace FSO.Vitaboy.Model
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VitaboyVertex : IVertexType
    {
        public Vector3 Position;
        public Vector2 TextureCoordinate;
        public Vector3 BvPosition; //blend vert
        public Vector3 Parameters;
        public Vector3 Normal;
        public Vector3 BvNormal;

        public VitaboyVertex(Vector3 position, Vector2 textureCoords, Vector3 bvPosition, Vector3 parameters, Vector3 normal, Vector3 bvNormal)
        {
            this.Position = position;
            this.TextureCoordinate = textureCoords;
            this.BvPosition = bvPosition;
            this.Parameters = parameters;
            this.Normal = normal;
            this.BvNormal = bvNormal;
        }

        public static int SizeInBytes = sizeof(float) * 17;

        public static VertexDeclaration VertexElements = new VertexDeclaration
        (
             new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
             new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
             new VertexElement(sizeof(float) * 5, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 1),
             new VertexElement(sizeof(float) * 8, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 2),
             new VertexElement(sizeof(float) * 11, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
             new VertexElement(sizeof(float) * 14, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 3)
        );

        VertexDeclaration IVertexType.VertexDeclaration
        {
            get { return VertexElements; }
        }
    }
}
