
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
using FSO.Content.Model;

namespace FSO.Common.Domain.Realestate
{
    public interface LotPricingStrategy
    {
        int GetPrice(CityMap map, ushort x, ushort y);
    }

    public class BasicLotPricingStrategy : LotPricingStrategy
    {
        public int GetPrice(CityMap map, ushort x, ushort y)
        {
            //TODO: Work on this scheme
            var terrain = map.GetTerrain(x, y);
            var basePrice = 3000;

            switch (terrain)
            {
                case TerrainType.GRASS:
                    basePrice += 1000;
                    break;
                case TerrainType.SAND:
                case TerrainType.SNOW:
                    basePrice += 2000;
                    break;
            }

            var price = basePrice;

            //Altitude increase price
            var elevation = map.GetElevation(x, y);

            //$19 for each elevation increment
            price += (19 * elevation);

            //+2500 for every water edge
            var leftLocation = MapCoordinates.Offset(new MapCoordinate(x, y), -1, 0);
            var leftTerrain = map.GetTerrain(leftLocation.X, leftLocation.Y);

            var rightLocation = MapCoordinates.Offset(new MapCoordinate(x, y), 1, 0);
            var rightTerrain = map.GetTerrain(rightLocation.X, rightLocation.Y);

            var topLocation = MapCoordinates.Offset(new MapCoordinate(x, y), 0, -1);
            var topTerrain = map.GetTerrain(topLocation.X, topLocation.Y);

            var bottomLocation = MapCoordinates.Offset(new MapCoordinate(x, y), 0, 1);
            var bottomTerrain = map.GetTerrain(bottomLocation.X, bottomLocation.Y);

            if (leftTerrain == TerrainType.WATER){
                price += 5000;
            }
            if (rightTerrain == TerrainType.WATER){
                price += 5000;
            }
            if (topTerrain == TerrainType.WATER){
                price += 5000;
            }
            if (bottomTerrain == TerrainType.WATER){
                price += 5000;
            }

            //Extra for an island
            if(bottomTerrain == TerrainType.WATER && topTerrain == TerrainType.WATER &&
                leftTerrain == TerrainType.WATER && rightTerrain == TerrainType.WATER){
                price += 10000;
            }

            return price;
        }
    }
}
