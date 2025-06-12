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

The Original Code is the TSO LoginServer.

The Initial Developer of the Original Code is
Mats 'Afr0' Vederhus. All Rights Reserved.

Contributor(s): ______________________________________.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarExtractor
{
    class Far3Entry
    {
        public uint DecompressedFileSize;
        public uint CompressedFileSize;
        public byte DataType;
        public uint DataOffset;
        public byte Compressed;
        public byte AccessNumber;
        //public ushort CompressedSpecifics;
        //public byte PowerValue;
        //public byte Unknown;
        //public ushort Unknown2;
        public ushort FilenameLength;
        public uint TypeID;
        public uint FileID;
        public string Filename;

        /*public int CalculateFileSize()
        {
            if (PowerValue == 0)
                return CompressedSpecifics;
            else if (PowerValue < 0)
                return (((PowerValue + 1) * 65536) + CompressedSpecifics);
            else if (PowerValue > 0)
                return ((PowerValue + 65536) + CompressedSpecifics);

            return 0;
        }*/
    }
}
