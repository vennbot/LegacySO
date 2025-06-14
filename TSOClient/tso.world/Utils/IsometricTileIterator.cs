
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

namespace FSO.LotView.Utils
{
    /// <summary>
    /// Utility to help iterating through tiles in a depth sorted order
    /// </summary>
    public class IsometricTileIterator
    {
        public static IEnumerable<IsometricTile> Tiles(WorldRotation rotation, short startX, short startY, short width, short height)
        {
            return Tiles(rotation, startX, startY, width, height, 1, 1);
        }

        public static IEnumerable<IsometricTile> Tiles(WorldRotation rotation, short startX, short startY, short width, short height, short advanceX, short advanceY){
            List<IsometricTile> tiles = new List<IsometricTile>();

            var endX = startX + width;
            var endY = startY + height;

            for (var x = startX; x < endX; x += advanceX)
            {
                for (var y = startY; y < endY; y += advanceY)
                {
                    tiles.Add(new IsometricTile 
                    {
                        TileX = x,
                        TileY = y
                    });
                }
            }

            tiles.Sort(new IsometricTileSorter<IsometricTile>(rotation));

            foreach (var tile in tiles)
            {
                yield return tile;
            }
        }

    }

    public class IsometricTileSorter<T> : IComparer<T> where T : IIsometricTile
    {

        private WorldRotation Rotation;
        public IsometricTileSorter(WorldRotation rotation){
            this.Rotation = rotation;
        }

        #region IComparer<IIsometricTile> Members
        public int Compare(T x, T y)
        {
            switch (Rotation){
                case WorldRotation.TopLeft:
                    return (x.TileX + x.TileY).CompareTo((y.TileX + y.TileY));
                case WorldRotation.TopRight:
                    return (x.TileX - x.TileY).CompareTo((y.TileX - y.TileY));
            }
            return 0;
        }
        #endregion
    }

    public interface IIsometricTile {
        short TileX { get; }
        short TileY { get; }
    }

    public class IsometricTile : IIsometricTile
    {
        public short TileX { get; set; }
        public short TileY { get; set; }
    }
}
