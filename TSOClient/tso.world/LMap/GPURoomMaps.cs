
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
using FSO.LotView.Model;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FSO.LotView.LMap
{
    public class GPURoomMaps : IDisposable
    {
        public List<Texture2D> RoomMaps;
        private GraphicsDevice GD;

        public GPURoomMaps(GraphicsDevice device)
        {
            GD = device;
        }

        public void Init(Blueprint blueprint)
        {
            var w = blueprint.Width;
            var h = blueprint.Height;

            RoomMaps = new List<Texture2D>();
            for (int i = 0; i < 5; i++) RoomMaps.Add(new Texture2D(GD, w, h, false, SurfaceFormat.Color));
        }

        public void SetRoomMap(sbyte floor, uint[] map)
        {
            RoomMaps[floor].SetData(map);
        }

        public void Dispose()
        {
            if (RoomMaps != null)
            {
                foreach (var map in RoomMaps)
                {
                    map?.Dispose();
                }
            }
        }
    }
}
