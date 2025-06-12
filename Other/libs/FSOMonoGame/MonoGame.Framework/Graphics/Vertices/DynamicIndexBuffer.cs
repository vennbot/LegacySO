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

using System;

namespace Microsoft.Xna.Framework.Graphics
{
	public class DynamicIndexBuffer : IndexBuffer
	{
        /// <summary>
        /// Special offset used internally by GraphicsDevice.DrawUserXXX() methods.
        /// </summary>
        internal int UserOffset;

        public bool IsContentLost { get { return false; } }
		
		public DynamicIndexBuffer(GraphicsDevice graphicsDevice, IndexElementSize indexElementSize, int indexCount, BufferUsage usage) :
			base(graphicsDevice, indexElementSize, indexCount, usage, true)
		{
		}

   		public DynamicIndexBuffer(GraphicsDevice graphicsDevice, Type indexType, int indexCount, BufferUsage usage) :
            base(graphicsDevice, indexType, indexCount, usage, true)
        {
        }

        public void SetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, SetDataOptions options) where T : struct
        {
            base.SetDataInternal<T>(offsetInBytes, data, startIndex, elementCount, options);
        }

        public void SetData<T>(T[] data, int startIndex, int elementCount, SetDataOptions options) where T : struct
        {
            base.SetDataInternal<T>(0, data, startIndex, elementCount, options);
        }
    }
}

