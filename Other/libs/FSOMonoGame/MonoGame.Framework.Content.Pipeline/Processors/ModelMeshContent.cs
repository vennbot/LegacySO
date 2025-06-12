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

using System.Collections.Generic;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace Microsoft.Xna.Framework.Content.Pipeline.Processors
{
    public sealed class ModelMeshContent
    {
        private BoundingSphere _boundingSphere;
        private ModelMeshPartContentCollection _meshParts;
        private string _name;
        private ModelBoneContent _parentBone;
        private MeshContent _sourceMesh;

        internal ModelMeshContent() { }

        internal ModelMeshContent(string name, MeshContent sourceMesh, ModelBoneContent parentBone,
                                  BoundingSphere boundingSphere, IList<ModelMeshPartContent> meshParts)
        {
            _name = name;
            _sourceMesh = sourceMesh;
            _parentBone = parentBone;
            _boundingSphere = boundingSphere;
            _meshParts = new ModelMeshPartContentCollection(meshParts);
        }

        public BoundingSphere BoundingSphere
        {
            get { return _boundingSphere; }
        }

        public ModelMeshPartContentCollection MeshParts
        {
            get { return _meshParts; }
        }

        public string Name
        {
            get { return _name; }
        }

        public ModelBoneContent ParentBone
        {
            get { return _parentBone; }
        }

        public MeshContent SourceMesh
        {
            get { return _sourceMesh; }
        }

        public object Tag { get; set; }
    }
}
