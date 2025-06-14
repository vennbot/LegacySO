
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
namespace FSO.SimAntics.Model.Routing
{
    public enum VMRouteFailCode : short
    {
        Success = 0,
        Unknown = 1,
        NoRoomRoute = 2,
        NoPath = 3, //pathfind failed
        Interrupted = 4,
        CantSit = 5,
        CantStand = 6, //with blocking object
        NoValidGoals = 7,
        DestTileOccupied = 8,
        DestChairOccupied = 9, //with blocking object
        NoChair = 10,
        WallInWay = 11, 
        AltsDontMatch = 12,
        DestTileOccupiedPerson = 13 //with blocking object
    }
}
