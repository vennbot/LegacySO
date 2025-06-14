
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
using Microsoft.Xna.Framework.Content;

namespace MSDFData
{
    public class KerningPair
    {
        [ContentSerializer] private readonly char LeftBackend;
        [ContentSerializer] private readonly char RightBackend;
        [ContentSerializer] private readonly float AdvanceBackend;

        public KerningPair()
        {
            
        }

        public KerningPair(char left, char right, float advance)
        {
            this.LeftBackend = left;
            this.RightBackend = right;
            this.AdvanceBackend = advance;
        }

        public char Left => this.LeftBackend;
        public char Right => this.RightBackend;
        public float Advance => this.AdvanceBackend;
    }
}
