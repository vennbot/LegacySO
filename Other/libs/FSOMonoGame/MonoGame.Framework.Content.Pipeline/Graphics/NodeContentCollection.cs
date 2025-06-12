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
    public class NodeContentCollection : ChildCollection<NodeContent, NodeContent>
    {
        /// <summary>
        /// Creates an instance of NodeContentCollection.
        /// </summary>
        /// <param name="parent">Parent object of the child objects returned in the collection.</param>
        internal NodeContentCollection(NodeContent parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Gets the parent of a child object.
        /// </summary>
        /// <param name="child">The child of the parent being retrieved.</param>
        /// <returns>The parent of the child object.</returns>
        protected override NodeContent GetParent(NodeContent child)
        {
            return child.Parent;
        }

        /// <summary>
        /// Modifies the value of the parent object of the specified child object.
        /// </summary>
        /// <param name="child">The child of the parent being modified.</param>
        /// <param name="parent">The new value for the parent object.</param>
        protected override void SetParent(NodeContent child, NodeContent parent)
        {
            child.Parent = parent;
        }
    }
}
