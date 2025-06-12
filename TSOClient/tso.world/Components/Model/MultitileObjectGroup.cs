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
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FSO.LotView.Components.Model
{
    public class MultitileObjectGroup
    {
        public List<ObjectComponent> Objects = new List<ObjectComponent>();

        public ObjectComponent DetermineComponentAt3D(ObjectComponent obj, Vector3 pos)
        {
            if (Objects.Count > 1)
            {
                BoundingBox bounds = obj.GetBounds();

                Vector3 size = bounds.Max - bounds.Min;

                // If this object is significantly bigger than a tile,
                // Try get the multitile part closest to the point of contact.
                if (size.X > 4f || size.Z > 4f)
                {
                    return DetermineClosestComponent(pos / 3f);
                }
            }

            return obj;
        }

        public ObjectComponent DetermineClosestComponent(Vector3 pos)
        {
            float bestDistance = float.PositiveInfinity;
            ObjectComponent bestObject = null;

            pos = new Vector3(pos.X, pos.Z, pos.Y) - new Vector3(0.5f, 0.5f, 0f);

            foreach (var subobj in Objects)
            {
                float dist = (subobj.Position - pos).Length();

                if (dist < bestDistance)
                {
                    bestDistance = dist;
                    bestObject = subobj;
                }
            }

            return bestObject;
        }
    }
}
