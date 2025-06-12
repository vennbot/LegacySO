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

using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Content.Pipeline.Graphics
{
    /// <summary>
    /// Provides methods for maintaining a list of index values.
    /// </summary>
    public sealed class IndexCollection : Collection<int>
    {
        /// <summary>
        /// Initializes a new instance of IndexCollection.
        /// </summary>
        public IndexCollection()
        {
        }

        /// <summary>
        /// Add a range of indices to the collection.
        /// </summary>
        /// <param name="indices">A collection of indices to add.</param>
        public void AddRange(IEnumerable<int> indices)
        {
            foreach (var t in indices)
                Add(t);
        }
    }
}
