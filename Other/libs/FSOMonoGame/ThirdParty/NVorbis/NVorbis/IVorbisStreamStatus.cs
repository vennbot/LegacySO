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
    public interface IVorbisStreamStatus
    {
        /// <summary>
        /// Gets the counters for latency and bitrate calculations, as well as overall bit counts
        /// </summary>
        void ResetStats();

        /// <summary>
        /// Gets the calculated bit rate of audio stream data for the everything decoded so far
        /// </summary>
        int EffectiveBitRate { get; }

        /// <summary>
        /// Gets the calculated bit rate for the last ~1 second of audio
        /// </summary>
        int InstantBitRate { get; }

        /// <summary>
        /// Gets the calculated latency per page
        /// </summary>
        TimeSpan PageLatency { get; }

        /// <summary>
        /// Gets the calculated latency per packet
        /// </summary>
        TimeSpan PacketLatency { get; }

        /// <summary>
        /// Gets the calculated latency per second of output
        /// </summary>
        TimeSpan SecondLatency { get; }

        /// <summary>
        /// Gets the number of bits read that do not contribute to the output audio
        /// </summary>
        long OverheadBits { get; }

        /// <summary>
        /// Gets the number of bits read that contribute to the output audio
        /// </summary>
        long AudioBits { get; }

        /// <summary>
        /// Gets the number of pages read so far in the current stream
        /// </summary>
        int PagesRead { get; }

        /// <summary>
        /// Gets the total number of pages in the current stream
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Gets whether the stream has been clipped since the last reset
        /// </summary>
        bool Clipped { get; }
    }
}
