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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Represents a set of bones associated with a model.
    /// </summary>
    public class ModelBoneCollection : ReadOnlyCollection<ModelBone>
    {
        public ModelBoneCollection(IList<ModelBone> list)
            : base(list)
        {

        }

        /// <summary>
        /// Retrieves a ModelBone from the collection, given the name of the bone.
        /// </summary>
        /// <param name="boneName">The name of the bone to retrieve.</param>
        public ModelBone this[string boneName]
        {
            get
            {
                ModelBone ret;
                if (!TryGetValue(boneName, out ret))
                    throw new KeyNotFoundException();
                return ret;
            }
        }

        /// <summary>
        /// Finds a bone with a given name if it exists in the collection.
        /// </summary>
        /// <param name="boneName">The name of the bone to find.</param>
        /// <param name="value">The bone named boneName, if found.</param>
        /// <returns>true if the bone was found</returns>
        public bool TryGetValue(string boneName, out ModelBone value)
        {
            if (string.IsNullOrEmpty(boneName))
                throw new ArgumentNullException("boneName");

            foreach (ModelBone bone in this)
            {
                if (bone.Name == boneName)
                {
                    value = bone;
                    return true;
                }
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Returns a ModelMeshCollection.Enumerator that can iterate through a ModelMeshCollection.
        /// </summary>
        /// <returns></returns>
        public new Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Provides the ability to iterate through the bones in an ModelMeshCollection.
        /// </summary>
        public struct Enumerator : IEnumerator<ModelBone>
        {
            private readonly ModelBoneCollection _collection;
            private int _position;

            internal Enumerator(ModelBoneCollection collection)
            {
                _collection = collection;
                _position = -1;
            }


            /// <summary>
            /// Gets the current element in the ModelMeshCollection.
            /// </summary>
            public ModelBone Current { get { return _collection[_position]; } }

            /// <summary>
            /// Advances the enumerator to the next element of the ModelMeshCollection.
            /// </summary>
            public bool MoveNext()
            {
                _position++;
                return (_position < _collection.Count);
            }

            #region IDisposable

            /// <summary>
            /// Immediately releases the unmanaged resources used by this object.
            /// </summary>
            public void Dispose()
            {
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return _collection[_position]; }
            }

            public void Reset()
            {
                _position = -1;
            }

            #endregion
        }
    }
}
