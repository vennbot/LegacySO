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
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VertexPositionColor : IVertexType
	{
        [DataMember]
		public Vector3 Position;
        
        [DataMember]
		public Color Color;

		public static readonly VertexDeclaration VertexDeclaration;

		public VertexPositionColor(Vector3 position, Color color)
		{
			this.Position = position;
			Color = color;
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
	            return (Position.GetHashCode() * 397) ^ Color.GetHashCode();
	        }
	    }

	    public override string ToString()
		{
            return "{{Position:" + this.Position + " Color:" + this.Color + "}}";
		}

		public static bool operator == (VertexPositionColor left, VertexPositionColor right)
		{
			return ((left.Color == right.Color) && (left.Position == right.Position));
		}

		public static bool operator != (VertexPositionColor left, VertexPositionColor right)
		{
			return !(left == right);
		}

		public override bool Equals(object obj)
		{
			if (obj == null) {
				return false;
			}
			if (obj.GetType () != base.GetType ()) {
				return false;
			}
			return (this == ((VertexPositionColor)obj));
		}

		static VertexPositionColor()
		{
			VertexElement[] elements = new VertexElement[] { new VertexElement (0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0), new VertexElement (12, VertexElementFormat.Color, VertexElementUsage.Color, 0) };
			VertexDeclaration declaration = new VertexDeclaration (elements);
			VertexDeclaration = declaration;
		}
	}
}
