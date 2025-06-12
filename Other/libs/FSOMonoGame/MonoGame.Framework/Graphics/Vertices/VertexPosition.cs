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

using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VertexPosition : IVertexType
	{
        [DataMember]
		public Vector3 Position;

		public static readonly VertexDeclaration VertexDeclaration;

		public VertexPosition(Vector3 position)
		{
			Position = position;
		}

		VertexDeclaration IVertexType.VertexDeclaration
        {
			get { return VertexDeclaration; }
		}

	    public override int GetHashCode()
	    {
	        return Position.GetHashCode();
	    }

		public override string ToString()
		{
            return "{{Position:" + Position + "}}";
		}

		public static bool operator == (VertexPosition left, VertexPosition right)
		{
			return left.Position == right.Position;
		}

		public static bool operator != (VertexPosition left, VertexPosition right)
		{
			return !(left == right);
		}

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return this == (VertexPosition) obj;
        }

        static VertexPosition()
		{
			VertexElement[] elements = { new VertexElement (0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0) };
            VertexDeclaration declaration = new VertexDeclaration(elements);
			VertexDeclaration = declaration;
		}
	}
}
