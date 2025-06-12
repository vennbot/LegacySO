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

namespace Iffinator.Flash
{
    public struct FieldEncodingData
    {
        public byte CompressionCode;
        public byte[] FieldWidths;
        public uint EncodedDataLength;
        public int FieldDataCounter;
        public byte[] EncodedData;

        public uint ReadDataLength;
        public byte BitBufferCount;
        public long BitBuffer;
    }

    /// <summary>
    /// Used to decode fields from a set of data compressed using field encoding 
    /// (http://simtech.sourceforge.net/tech/misc.html#fields). Original code
    /// written by Propeng.
    /// </summary>
    public class FieldReader
    {
        private int ReadBits(ref FieldEncodingData FieldData, byte Width, ref long Value)
        {
            while (FieldData.BitBufferCount < Width)
            {
                if (FieldData.ReadDataLength >= FieldData.EncodedDataLength)
                    return 0;

                FieldData.BitBuffer <<= 8;
                FieldData.BitBuffer |= FieldData.EncodedData[FieldData.FieldDataCounter++];
                FieldData.BitBufferCount += 8;
                FieldData.ReadDataLength++;
            }

            Value = FieldData.BitBuffer >> (FieldData.BitBufferCount - Width);
            Value &= (1L << Width) - 1;
            FieldData.BitBufferCount -= Width;

            return 1;
        }

        public int DecodeField(ref FieldEncodingData Data, byte FieldType, ref long Value)
        {
            long Prefix = 0, Width = 0;

            if(ReadBits(ref Data, 1, ref Value) == 0)
                return 0;

            if (Value == 0)
                return 1;

            if (ReadBits(ref Data, 2, ref Prefix) == 0)
                return 0;

            Width = Data.FieldWidths[FieldType * 4 + Prefix];

            if(ReadBits(ref Data, (byte)Width, ref Value) == 0)
                return 0;

            Value |= -(Value & 1L << (byte)(Width - 1));

            return 1;
        }
    }
}
