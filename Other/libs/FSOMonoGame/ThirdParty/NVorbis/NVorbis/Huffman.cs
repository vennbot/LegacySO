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
/****************************************************************************
 * NVorbis                                                                  *
 * Copyright (C) 2014, Andrew Ward <afward@gmail.com>                       *
 *                                                                          *
 * See COPYING for license terms (Ms-PL).                                   *
 *                                                                          *
 ***************************************************************************/
using System;

namespace NVorbis
{
    static class Huffman
    {
        const int MAX_TABLE_BITS = 10;

        static internal System.Collections.Generic.List<HuffmanListNode> BuildPrefixedLinkedList(int[] values, int[] lengthList, int[] codeList, out int tableBits, out HuffmanListNode firstOverflowNode)
        {
            HuffmanListNode[] list = new HuffmanListNode[lengthList.Length];

            var maxLen = 0;
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = new HuffmanListNode
                {
                    Value = values[i],
                    Length = lengthList[i] <= 0 ? 99999 : lengthList[i],
                    Bits = codeList[i],
                    Mask = (1 << lengthList[i]) - 1,
                };
                if (lengthList[i] > 0 && maxLen < lengthList[i])
                {
                    maxLen = lengthList[i];
                }
            }

            Array.Sort(list, SortCallback);

            tableBits = maxLen > MAX_TABLE_BITS ? MAX_TABLE_BITS : maxLen;

            var prefixList = new System.Collections.Generic.List<HuffmanListNode>(1 << tableBits);
            firstOverflowNode = null;
            for (int i = 0; i < list.Length && list[i].Length < 99999; i++)
            {
                if (firstOverflowNode == null)
                {
                    var itemBits = list[i].Length;
                    if (itemBits > tableBits)
                    {
                        firstOverflowNode = list[i];
                    }
                    else
                    {
                        var maxVal = 1 << (tableBits - itemBits);
                        var item = list[i];
                        for (int j = 0; j < maxVal; j++)
                        {
                            var idx = (j << itemBits) | item.Bits;
                            while (prefixList.Count <= idx)
                            {
                                prefixList.Add(null);
                            }
                            prefixList[idx] = item;
                        }
                    }
                }
                else
                {
                    list[i - 1].Next = list[i];
                }
            }

            while (prefixList.Count < 1 << tableBits)
            {
                prefixList.Add(null);
            }

            return prefixList;
        }

        static int SortCallback(HuffmanListNode i1, HuffmanListNode i2)
        {
            var len = i1.Length - i2.Length;
            if (len == 0)
            {
                return i1.Bits - i2.Bits;
            }
            return len;
        }
    }

    class HuffmanListNode
    {
        internal int Value;

        internal int Length;
        internal int Bits;
        internal int Mask;

        internal HuffmanListNode Next;
    }
}
