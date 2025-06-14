
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
using Microsoft.Xna.Framework;

namespace FSO.LotView.LMap
{
    internal struct ClockwisePoints4
    {
        public Vector2 Pt0;
        public Vector2 Pt1;
        public Vector2 Pt2;
        public Vector2 Pt3;

        public Vector2 this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Pt0;
                    case 1: return Pt1;
                    case 2: return Pt2;
                    case 3: return Pt3;
                    default: return default;
                }
            }

            set
            {
                switch (index)
                {
                    case 0: Pt0 = value; break;
                    case 1: Pt1 = value; break;
                    case 2: Pt2 = value; break;
                    case 3: Pt3 = value; break;
                }
            }
        }
    }

    internal struct ClockwisePoints
    {
        public Vector2 Pt0;
        public Vector2 Pt1;
        public Vector2 Pt2;

        public ClockwisePoints(Vector2 pt0, Vector2 pt1, Vector2 pt2)
        {
            Pt0 = pt0;
            Pt1 = pt1;
            Pt2 = pt2;
        }

        public Vector2 this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Pt0;
                    case 1: return Pt1;
                    case 2: return Pt2;
                    default: return default;
                }
            }

            set
            {
                switch (index)
                {
                    case 0: Pt0 = value; break;
                    case 1: Pt1 = value; break;
                    case 2: Pt2 = value; break;
                }
            }
        }
    }
}
