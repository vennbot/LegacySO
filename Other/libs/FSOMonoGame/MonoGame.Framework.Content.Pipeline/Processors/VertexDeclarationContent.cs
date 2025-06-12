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
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content.Pipeline.Processors
{
    /// <summary>
    /// Provides methods and properties for maintaining the vertex declaration data of a VertexContent.
    /// </summary>
    public class VertexDeclarationContent : ContentItem
    {
        Collection<VertexElement> vertexElements;
        int? vertexStride;

        /// <summary>
        /// Gets the VertexElement object of the vertex declaration.
        /// </summary>
        /// <value>The VertexElement object of the vertex declaration.</value>
        public Collection<VertexElement> VertexElements { get { return vertexElements; } }

        /// <summary>
        /// The number of bytes from one vertex to the next.
        /// </summary>
        /// <value>The stride (in bytes).</value>
        public int? VertexStride
        {
            get { return vertexStride; }
            set { vertexStride = value; }
        }

        /// <summary>
        /// Initializes a new instance of VertexDeclarationContent.
        /// </summary>
        public VertexDeclarationContent()
        {
            vertexElements = new Collection<VertexElement>();
        }
    }
}
