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

namespace Microsoft.Xna.Framework.Media
{
    public sealed class Artist : IDisposable
    {
        private string artist;

        /// <summary>
        /// Gets the AlbumCollection for the Artist.
        /// </summary>
        public AlbumCollection Albums
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the object is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the name of the Artist.
        /// </summary>
        public string Name
        {
            get
            {
                return this.artist;
            }
        }

        /// <summary>
        /// Gets the SongCollection for the Artist.
        /// </summary>
        public SongCollection Songs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Artist(string artist)
        {
            this.artist = artist;
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by this object.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Returns a String representation of the Artist.
        /// </summary>
        public override string ToString()
        {
            return this.artist.ToString();
        }

        /// <summary>
        /// Gets the hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return this.artist.GetHashCode();
        }
    }
}
