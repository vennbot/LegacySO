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
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CityRenderer
{
    /// <summary>
    /// Represents a MeshVertex that makes up a face.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MeshVertex
    {
        public Vector3 Coord;
        /** UV Mapping **/
        public Vector2 TextureCoord;
        public Vector2 Texture2Coord;
        public Vector2 Texture3Coord;
        public Vector2 UVBCoord;
        public Vector2 RoadCoord;
        public Vector2 RoadCCoord;

        public static int SizeInBytes = sizeof(float) * 15;

        public static readonly VertexElement[] VertexElements =
        {
            new VertexElement(0,0, VertexElementFormat.Vector3,
                VertexElementMethod.Default, VertexElementUsage.Position, 0),
            new VertexElement(0,sizeof(float)*3, VertexElementFormat.Vector2,
                VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 0),
            new VertexElement(0,sizeof(float)*(3+2), VertexElementFormat.Vector2,
                VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 1),
            new VertexElement(0, sizeof(float)*(3+4), VertexElementFormat.Vector2, 
                VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 2),
            new VertexElement(0, sizeof(float)*(3+6), VertexElementFormat.Vector2, 
                VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 3),
            new VertexElement(0, sizeof(float)*(3+8), VertexElementFormat.Vector2, 
                VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 4),
            new VertexElement(0, sizeof(float)*(3+10), VertexElementFormat.Vector2, 
                VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 5)
        };
    }
}
