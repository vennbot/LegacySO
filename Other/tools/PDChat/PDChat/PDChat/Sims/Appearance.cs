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

The Original Code is the TSOClient.

The Initial Developer of the Original Code is
ddfczm. All Rights Reserved.

Contributor(s): ______________________________________.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TSO.Common.utils;
using TSO.Files.utils;
using TSO.Common.content;

namespace PDChat.Sims
{
    /// <summary>
    /// Represents an appearance for a model.
    /// </summary>
    public class Appearance
    {
        public uint ThumbnailTypeID;
        public uint ThumbnailFileID;
        public AppearanceBinding[] Bindings;

        public ContentID ThumbnailID
        {
            get
            {
                return new ContentID(ThumbnailTypeID, ThumbnailFileID);
            }
        }

        public void Read(Stream stream)
        {
            using (var io = IoBuffer.FromStream(stream)){
                var version = io.ReadUInt32();

                ThumbnailFileID = io.ReadUInt32();
                ThumbnailTypeID = io.ReadUInt32();

                var numBindings = io.ReadUInt32();
                Bindings = new AppearanceBinding[numBindings];

                for (var i = 0; i < numBindings; i++){
                    Bindings[i] = new AppearanceBinding {
                        FileID = io.ReadUInt32(),
                        TypeID = io.ReadUInt32()
                    };
                }
            }
        }
    }

    public class AppearanceBinding
    {
        public uint TypeID;
        public uint FileID;
    }
}
