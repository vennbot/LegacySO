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
using System.IO;

namespace NVorbis
{
    abstract class VorbisTime
    {
        internal static VorbisTime Init(VorbisStreamDecoder vorbis, DataPacket packet)
        {
            var type = (int)packet.ReadBits(16);

            VorbisTime time = null;
            switch (type)
            {
                case 0: time = new Time0(vorbis); break;
            }
            if (time == null) throw new InvalidDataException();

            time.Init(packet);
            return time;
        }

        VorbisStreamDecoder _vorbis;

        protected VorbisTime(VorbisStreamDecoder vorbis)
        {
            _vorbis = vorbis;
        }

        abstract protected void Init(DataPacket packet);

        class Time0 : VorbisTime
        {
            internal Time0(VorbisStreamDecoder vorbis) : base(vorbis) { }

            protected override void Init(DataPacket packet)
            {
                
            }
        }
    }
}
