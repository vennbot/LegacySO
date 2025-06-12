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
using Microsoft.Xna.Framework.Graphics;

namespace TwoMGFX
{
	internal partial class ShaderData
	{
		public ShaderData(bool isVertexShader, int sharedIndex, byte[] bytecode)
		{
			IsVertexShader = isVertexShader;
			SharedIndex = sharedIndex;
			Bytecode = (byte[])bytecode.Clone();	    
		}

		public bool IsVertexShader { get; private set; }

		public struct Sampler
		{
			public MojoShader.MOJOSHADER_samplerType type;
			public int textureSlot;
            public int samplerSlot;
			public string samplerName;
			public string parameterName;
			public int parameter;
			public SamplerState state;
		}

		public struct Attribute
		{
            public string name;
            public VertexElementUsage usage;
			public int index;
#pragma warning disable 649
            public int location;
#pragma warning restore 649
        }

		/// <summary>
		/// The index to the constant buffers which are 
		/// required by this shader at runtime.
		/// </summary>
		public int[] _cbuffers;

		public Sampler[] _samplers;

		public Attribute[] _attributes;

		public byte[] ShaderCode { get; set; }


#region Non-Serialized Stuff

		public byte[] Bytecode { get; private set; }

		// The index of the shader in the shared list.
		public int SharedIndex { get; private set; }

#endregion // Non-Serialized Stuff

	}
}

