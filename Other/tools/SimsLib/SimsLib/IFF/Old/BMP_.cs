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
/*This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
If a copy of the MPL was not distributed with this file, You can obtain one at
http://mozilla.org/MPL/2.0/.

The Original Code is the SimsLib.

The Initial Developer of the Original Code is
Mats 'Afr0' Vederhus. All Rights Reserved.

Contributor(s):
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace SimsLib.IFF
{
    /// <summary>
    /// Represents a BMP_ chunk.
    /// A BMP_ chunk is like a regular bitmap file.
    /// </summary>
    class BMP_ : IffChunk
    {
        private Bitmap m_BitmapData;

        /// <summary>
        /// Creates a new BMP_ file.
        /// </summary>
        /// <param name="Chunk">The chunk to create the BMP_ file from.</param>
        public BMP_(IffChunk Chunk) : base(Chunk)
        {
            MemoryStream MemStream = new MemoryStream(Chunk.Data);

            m_BitmapData = new Bitmap(MemStream);
        }
    }
}
