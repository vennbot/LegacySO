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
using Microsoft.Xna.Framework;

namespace FSO.SimAntics.Model.Routing
{
    public class VMEntityObstacle : VMObstacle
    {
        public VMEntity Parent;
        public VMObstacleSet Set;
        public List<VMObstacle> Dynamic;

        public VMEntityObstacle() { }

        public VMEntityObstacle(Point source, Point dest) : base(source, dest)
        {
        }

        public VMEntityObstacle(int x1, int y1, int x2, int y2, VMEntity ent) : base(x1, y1, x2, y2)
        {
            Parent = ent;
        }

        public void Unregister()
        {
            if (Parent.StaticFootprint) Set?.Delete(this);
            else Dynamic?.Remove(this);
            Set = null;
            Dynamic = null;
        }
    }
}
