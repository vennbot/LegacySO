
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

namespace FSO.Common.Domain.Realestate
{
    public class MapCoordinates
    {
        public static MapCoordinate Offset(ushort x, ushort y, int offsetX, int offsetY)
        {
            return Offset(new MapCoordinate(x, y), offsetX, offsetY);
        }

        public static MapCoordinate Offset(MapCoordinate coord, int offsetX, int offsetY)
        {
            //Tile above = 0, -1
            //Tile below = 0, 1
            //Tile left = -1, 0
            //Tile right = 1, 0
            return new MapCoordinate((ushort)(coord.X - offsetY), (ushort)(coord.Y + offsetX));
        }

        public static bool InBounds(ushort x, ushort y){
            return InBounds(x, y, 0);
        }

        public static bool InBounds(ushort x, ushort y, ushort padding)
        {
            if (y < padding) { return false; }
            if (y > (511 - padding)) { return false; }
            
            var xStart = 0;
            var xEnd = 0;

            if (y < 306){
                xStart = 306 - y;
            }else{
                xStart = y - 306;
            }

            if (y < 205){
                xEnd = 307 + y;
            }else{
                xEnd = 512 - (y - 205);
            }

            if (x < xStart + padding) { return false; }
            if (x > xEnd - padding) { return false; }

            return true;
        }

        public static uint Pack(ushort x, ushort y)
        {
            return (uint)(x << 16 | y);
        }

        public static MapCoordinate Unpack(uint value)
        {
            var x = value >> 16;
            var y = value & 0xFFFF;
            return new MapCoordinate((ushort)x, (ushort)y);
        }
    }

    public struct MapCoordinate
    {
        public MapCoordinate(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        public ushort X;
        public ushort Y;

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }
    }
}
