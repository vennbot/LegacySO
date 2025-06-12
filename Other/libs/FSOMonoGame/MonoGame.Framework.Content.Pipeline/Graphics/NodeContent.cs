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
    /// Provides a base class for graphics types that define local coordinate systems.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Node '{Name}'")]
    public class NodeContent : ContentItem
    {
        Matrix transform;
        NodeContent parent;
        NodeContentCollection children;
        AnimationContentDictionary animations;

        /// <summary>
        /// Gets the value of the local Transform property, multiplied by the AbsoluteTransform of the parent.
        /// </summary>
        public Matrix AbsoluteTransform
        {
            get
            {
                if (parent != null)
                    return transform * parent.AbsoluteTransform;
                return transform;
            }
        }

        /// <summary>
        /// Gets the set of animations belonging to this node.
        /// </summary>
        public AnimationContentDictionary Animations
        {
            get
            {
                return animations;
            }
        }

        /// <summary>
        /// Gets the children of the NodeContent object.
        /// </summary>
        public NodeContentCollection Children
        {
            get
            {
                return children;
            }
        }

        /// <summary>
        /// Gets the parent of this NodeContent object.
        /// </summary>
        public NodeContent Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        /// <summary>
        /// Gets the transform matrix of the scene.
        /// The transform matrix defines a local coordinate system for the content in addition to any children of this object.
        /// </summary>
        public Matrix Transform
        {
            get
            {
                return transform;
            }
            set
            {
                transform = value;
            }
        }

        /// <summary>
        /// Creates an instance of NodeContent.
        /// </summary>
        public NodeContent()
        {
            children = new NodeContentCollection(this);
            animations = new AnimationContentDictionary();
            Transform = Matrix.Identity;
        }
    }
}
