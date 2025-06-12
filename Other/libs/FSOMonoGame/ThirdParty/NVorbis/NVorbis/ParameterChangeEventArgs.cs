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

namespace NVorbis
{
    /// <summary>
    /// Event data for when a logical stream has a parameter change.
    /// </summary>
    [Serializable]
    public class ParameterChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new instance of <see cref="ParameterChangeEventArgs"/>.
        /// </summary>
        /// <param name="firstPacket">The first packet after the parameter change.</param>
        public ParameterChangeEventArgs(DataPacket firstPacket)
        {
            FirstPacket = firstPacket;
        }

        /// <summary>
        /// Gets the first packet after the parameter change.  This would typically be the parameters packet.
        /// </summary>
        public DataPacket FirstPacket { get; private set; }
    }
}
