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

namespace Microsoft.Xna.Framework.Graphics
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPositionNormalTexture : IVertexType
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoordinate;
        public static readonly VertexDeclaration VertexDeclaration;
        public VertexPositionNormalTexture(Vector3 position, Vector3 normal, Vector2 textureCoordinate)
        {
            this.Position = position;
            this.Normal = normal;
            this.TextureCoordinate = textureCoordinate;
        }

        VertexDeclaration IVertexType.VertexDeclaration
        {
            get
            {
                return VertexDeclaration;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Position.GetHashCode();
                hashCode = (hashCode * 397) ^ Normal.GetHashCode();
                hashCode = (hashCode * 397) ^ TextureCoordinate.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return "{{Position:" + this.Position + " Normal:" + this.Normal + " TextureCoordinate:" + this.TextureCoordinate + "}}";
        }

        public static bool operator ==(VertexPositionNormalTexture left, VertexPositionNormalTexture right)
        {
            return (((left.Position == right.Position) && (left.Normal == right.Normal)) && (left.TextureCoordinate == right.TextureCoordinate));
        }

        public static bool operator !=(VertexPositionNormalTexture left, VertexPositionNormalTexture right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != base.GetType())
            {
                return false;
            }
            return (this == ((VertexPositionNormalTexture)obj));
        }

        static VertexPositionNormalTexture()
        {
            VertexElement[] elements = new VertexElement[] { new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0), new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0), new VertexElement(0x18, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0) };
            VertexDeclaration declaration = new VertexDeclaration(elements);
            VertexDeclaration = declaration;
        }
    }
}
