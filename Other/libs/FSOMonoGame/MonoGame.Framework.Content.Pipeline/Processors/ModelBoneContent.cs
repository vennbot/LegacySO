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

namespace Microsoft.Xna.Framework.Content.Pipeline.Processors
{
    public sealed class ModelBoneContent
    {
        private ModelBoneContentCollection _children;
        private int _index;
        private string _name;
        private ModelBoneContent _parent;
        private Matrix _transform;

        internal ModelBoneContent() { }

        internal ModelBoneContent(string name, int index, Matrix transform, ModelBoneContent parent)
        {
            _name = name;
            _index = index;
            _transform = transform;
            _parent = parent;
        }

        public ModelBoneContentCollection Children
        {
            get { return _children; }
            internal set { _children = value; }
        }

        public int Index
        {
            get { return _index; }
        }

        public string Name
        {
            get { return _name; }
        }

        public ModelBoneContent Parent
        {
            get { return _parent; }
        }

        public Matrix Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }
    }
}
