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

using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content.Pipeline.Processors
{
    public sealed class ModelMeshPartContent
    {
        private IndexCollection _indexBuffer;
        private MaterialContent _material;
        private int _numVertices;
        private int _primitiveCount;
        private int _startIndex;
        private VertexBufferContent _vertexBuffer;
        private int _vertexOffset;

        internal ModelMeshPartContent() { }

        internal ModelMeshPartContent(VertexBufferContent vertexBuffer, IndexCollection indices, int vertexOffset,
                                      int numVertices, int startIndex, int primitiveCount)
        {
            _vertexBuffer = vertexBuffer;
            _indexBuffer = indices;
            _vertexOffset = vertexOffset;
            _numVertices = numVertices;
            _startIndex = startIndex;
            _primitiveCount = primitiveCount;
        }

        public IndexCollection IndexBuffer
        {
            get { return _indexBuffer; }
        }

        public MaterialContent Material
        {
            get { return _material; }
            set { _material = value; }
        }

        public int NumVertices
        {
            get { return _numVertices; }
        }

        public int PrimitiveCount
        {
            get { return _primitiveCount; }
        }

        public int StartIndex
        {
            get { return _startIndex; }
        }

        public object Tag { get; set; }

        public VertexBufferContent VertexBuffer
        {
            get { return _vertexBuffer; }
        }

        public int VertexOffset
        {
            get { return _vertexOffset; }
        }
    }
}
