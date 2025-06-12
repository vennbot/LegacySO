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
using System.Collections.Generic;

namespace FSO.Server.Debug.PacketAnalyzer
{
    public class ByteCountPacketAnalyzer : IPacketAnalyzer
    {
        #region IPacketAnalyzer Members

        public List<PacketAnalyzerResult> Analyze(byte[] data)
        {
            var result = new List<PacketAnalyzerResult>();

            for (var i = 0; i < data.Length; i++)
            {
                if (i + 4 < data.Length)
                {
                    byte len1 = data[i];
                    byte len2 = data[i + 1];
                    byte len3 = data[i + 2];
                    byte len4 = data[i + 3];

                    long len = len1 << 24 | len2 << 16 | len3 << 8 | len4;

                    if (len == data.Length - (i + 4))
                    {
                        result.Add(new PacketAnalyzerResult
                        {
                            Offset = i,
                            Length = 4,
                            Description = "byte-count(" + len + ")"
                        });
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
