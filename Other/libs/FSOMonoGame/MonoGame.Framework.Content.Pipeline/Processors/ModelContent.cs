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

namespace Microsoft.Xna.Framework.Content.Pipeline.Processors
{
    public sealed class ModelContent
    {
        private ModelBoneContentCollection _bones;
        private ModelMeshContentCollection _meshes;
        private ModelBoneContent _root;

        internal ModelContent() { }

        internal ModelContent(ModelBoneContent root, IList<ModelBoneContent> bones, IList<ModelMeshContent> meshes)
        {
            _root = root;
            _bones = new ModelBoneContentCollection(bones);
            _meshes = new ModelMeshContentCollection(meshes);
        }

        public ModelBoneContentCollection Bones
        {
            get { return _bones; }
        }

        public ModelMeshContentCollection Meshes
        {
            get { return _meshes; }
        }

        public ModelBoneContent Root
        {
            get { return _root; }
        }

        public object Tag { get; set; }
    }
}
