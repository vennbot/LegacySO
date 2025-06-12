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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NVorbis.Ogg
{
    class Crc
    {
        const uint CRC32_POLY = 0x04c11db7;
        static uint[] crcTable = new uint[256];

        static Crc()
        {
            for (uint i = 0; i < 256; i++)
            {
                uint s = i << 24;
                for (int j = 0; j < 8; ++j)
                {
                    s = (s << 1) ^ (s >= (1U << 31) ? CRC32_POLY : 0);
                }
                crcTable[i] = s;
            }
        }

        uint _crc;

        public Crc()
        {
            Reset();
        }

        public void Reset()
        {
            _crc = 0U;
        }

        public void Update(int nextVal)
        {
            _crc = (_crc << 8) ^ crcTable[nextVal ^ (_crc >> 24)];
        }

        public bool Test(uint checkCrc)
        {
            return _crc == checkCrc;
        }
    }
}
