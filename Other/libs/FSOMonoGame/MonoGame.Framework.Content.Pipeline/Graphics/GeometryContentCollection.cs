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

namespace Microsoft.Xna.Framework.Content.Pipeline.Graphics
{
    /// <summary>
    /// Provides methods for maintaining a collection of geometry batches that make up a mesh.
    /// </summary>
    public sealed class GeometryContentCollection : ChildCollection<MeshContent, GeometryContent>
    {
        internal GeometryContentCollection(MeshContent parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Gets the parent of a child object.
        /// </summary>
        /// <param name="child">The child of the parent being retrieved.</param>
        /// <returns>The parent of the child object.</returns>
        protected override MeshContent GetParent(GeometryContent child)
        {
            return child.Parent;
        }

        /// <summary>
        /// Sets the parent of the specified child object.
        /// </summary>
        /// <param name="child">The child of the parent being set.</param>
        /// <param name="parent">The parent of the child object.</param>
        protected override void SetParent(GeometryContent child, MeshContent parent)
        {
            child.Parent = parent;
        }
    }
}
