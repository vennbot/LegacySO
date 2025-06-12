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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;

namespace FSO.LotView.RC
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WallVertexRC : IVertexType
    {
        public Vector3 Position;
        public Vector4 Color;
        public Vector3 Info;

        public WallVertexRC(Vector3 position, Vector4 color, Vector3 info)
        {
            this.Position = position;
            this.Color = color;
            this.Info = info;
        }

        public static int SizeInBytes = sizeof(float) * 10;

        public static VertexDeclaration VertexElements = new VertexDeclaration
        (
             new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
             new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector4, VertexElementUsage.Color, 0),
             new VertexElement(sizeof(float) * 7, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
        );

        VertexDeclaration IVertexType.VertexDeclaration
        {
            get { return VertexElements; }
        }
    }
}
