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

namespace Microsoft.Xna.Framework.Media
{
    public sealed class AlbumCollection : IDisposable
    {
        private List<Album> albumCollection;

        /// <summary>
        /// Gets the number of Album objects in the AlbumCollection.
        /// </summary>
        public int Count
        {
            get
            {
                return this.albumCollection.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the object is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                return false;
            }
        }

        public AlbumCollection(List<Album> albums)
        {
            this.albumCollection = albums;
        }

        /// <summary>
        /// Gets the Album at the specified index in the AlbumCollection.
        /// </summary>
        /// <param name="index">Index of the Album to get.</param>
        public Album this[int index]
        {
            get
            {
                return this.albumCollection[index];
            }
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by this object.
        /// </summary>
        public void Dispose()
        {
            foreach (var album in this.albumCollection)
                album.Dispose();
        }
    }
}
