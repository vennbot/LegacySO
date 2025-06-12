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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSOClient.Code.Rendering.Lot.Model;
using TSOClient.Code.Data;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TSOClient.Code.Rendering.Lot.Components
{
    public class WallComponent : House2DComponent
    {
        private HouseDataWall WallInfo;
        public int Level;

        public WallComponent(HouseDataWall wallInfo)
        {
            this.WallInfo = wallInfo;
        }

        //public override void OnStateChanged(HouseRenderState state)
        //{
        //    base.OnStateChanged(state);

        //    /** Change texture pointers **/

        //}

        public override int Height{
            get { return 0;  }
        }

        public override void Draw(HouseRenderState state, HouseBatch batch)
        {
            var position = state.TileToScreen(Position);



            if ((WallInfo.Segments & WallSegments.BottomLeft) == WallSegments.BottomLeft)
            {
                var wall = ArchitectureCatalog.GetWallPattern(WallInfo.BottomLeftPattern);
                if (wall != null)
                {
                    var tx = wall.Far.RightTexture;

                    //batch.Draw(tx, new Rectangle((int)position.X, (int)position.Y - 49, 16, 67), Color.White);
                    //batch.Draw(tx, new Rectangle((int)position.X, (int)position.Y - 49, 16, 67), Color.White);
                }
            }

            if ((WallInfo.Segments & WallSegments.BottomRight) == WallSegments.BottomRight)
            {
                var wall = ArchitectureCatalog.GetWallPattern(WallInfo.BottomRightPattern);
                if (wall != null)
                {
                    var tx = wall.Far.LeftTexture;

                    //batch.Draw(tx, new Rectangle((int)position.X, (int)position.Y - 49, 16, 67), Color.White);
                    //batch.Draw(tx, new Rectangle((int)position.X + 16, (int)position.Y - 49, 16, 67), Color.White);
                }
            }





            //if ((WallInfo.Segments & WallSegments.BottomRight) == WallSegments.BottomRight)
            //{
            //    var wall = ArchitectureCatalog.GetWallPattern(WallInfo.BottomRightPattern);
            //    if (wall != null)
            //    {
            //        var tx = wall.Far.LeftTexture;

            //        //batch.Draw(tx, new Rectangle((int)position.X, (int)position.Y - 49, 16, 67), Color.White);
            //        batch.Draw(tx, new Rectangle((int)position.X + 16, (int)position.Y - 49, 16, 67), Color.White);
            //    }
            //}


            //if ((WallInfo.Segments & WallSegments.BottomRight) == WallSegments.BottomRight)
            //{
            //    var wall = ArchitectureCatalog.GetWallPattern(WallInfo.BottomLeftPattern);
            //    if (wall != null)
            //    {
            //        var tx = wall.Far.LeftTexture;
            //        batch.Draw(tx, new Rectangle((int)position.X, (int)position.Y - 58, 16, 67), Color.White);
            //    }
            //}
        }
    }
}
