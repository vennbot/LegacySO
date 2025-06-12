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
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Dressup
{
    class PurchasableObject
    {
        private uint m_Version;
        private uint m_Gender;          //0 if male, 1 if female.
        //Outfit/Data type - A 4-byte unsigned integer specifying the type of the data to follow; 
        //should be 8 for Asset if the outfit is specified and 0 if not 
        private uint m_AssetType;
        private ulong m_OutfitAssetID;

        public ulong OutfitID
        {
            get { return m_OutfitAssetID; }
        }

        public PurchasableObject(byte[] FileData)
        {
            MemoryStream MemStream = new MemoryStream(FileData);
            BinaryReader Reader = new BinaryReader(MemStream);

            m_Version = Endian.SwapUInt32(Reader.ReadUInt32());
            m_Gender = Endian.SwapUInt32(Reader.ReadUInt32());
            m_AssetType = Endian.SwapUInt32(Reader.ReadUInt32());

            Reader.ReadUInt32(); //GroupID

            m_OutfitAssetID = Convert.ToUInt64(Endian.SwapUInt64(Reader.ReadUInt64()));

            Reader.Close();
        }
    }
}
