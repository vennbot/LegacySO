
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.InteropServices;

namespace FSO.LotView.Utils
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TerrainVertex : IVertexType
    {
        public Vector3 Position;
        public Vector4 Color;
        public Vector3 GrassInfo;
        public Vector3 Normal;

        public TerrainVertex(Vector3 position, Vector4 color, Vector2 grassPos, Single live, Vector3 normal)
        {
            this.Normal = normal;
            this.Position = position;
            this.Color = color;
            this.GrassInfo = new Vector3(live, grassPos.X, grassPos.Y);
        }

        public TerrainVertex(Vector3 position, Vector4 color, Vector2 grassPos, Single live) : this(position, color, grassPos, live, Vector3.One)
        {
        }

        public static int SizeInBytes = sizeof(float) * 13;

        public static VertexDeclaration VertexElements = new VertexDeclaration
        (
             new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
             new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector4, VertexElementUsage.Color, 0),
             new VertexElement(sizeof(float) * 7, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0),
             new VertexElement(sizeof(float) * 10, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 1)
        );

        VertexDeclaration IVertexType.VertexDeclaration
        {
            get { return VertexElements; }
        }
    }
}
