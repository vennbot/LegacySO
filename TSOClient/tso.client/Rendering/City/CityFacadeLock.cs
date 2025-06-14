
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
using System;
using System.Collections.Generic;

namespace FSO.Client.Rendering.City
{
    /// <summary>
    /// Data structure for locking facades near the current lot into memory.
    /// Not doing this would require a scan around the current lot of like 48x48 tiles every frame, which is really just wasting cpu cycles.
    /// Also includes bounding boxes for them, so they can be frustrum culled for MAXIMUM PERFORMANCE.
    /// </summary>
    public class CityFacadeLock : IDisposable
    {
        public List<CityFacadeEntry> Entries = new List<CityFacadeEntry>();

        public CityFacadeLock()
        {
        }

        public void Dispose()
        {
            foreach (var entry in Entries)
            {
                entry.LotImg.Held--;
            }
            Entries.Clear();
        }
    }

    public class CityFacadeEntry
    {
        public LotThumbEntry LotImg;
        public Vector3 Position;
        public BoundingBox Bounds;
        public Vector2 Location;
    }
}
