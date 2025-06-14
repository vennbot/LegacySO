
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
namespace FSO.SimAntics.Engine.Scopes
{
    public enum VMVariableScope {
        // See http://simantics.wikidot.com/wiki:scopes for more info.

        MyObjectAttributes = 0,
        StackObjectAttributes = 1,
        TargetObjectAttributes = 2, //unused
        MyObject = 3,
        StackObject = 4,
        TargetObject = 5, //unused
        Global = 6,
        Literal = 7,
        Temps = 8,
        Parameters = 9,
        StackObjectID = 10,
        TempByTemp = 11,
        TreeAdRange = 12,
        StackObjectTemp = 13,
        MyMotives = 14,
        StackObjectMotives = 15,
        StackObjectSlot = 16,
        StackObjectMotiveByTemp = 17,
        MyPersonData = 18,
        StackObjectPersonData = 19,
        MySlot = 20,
        StackObjectDefinition = 21,
        StackObjectAttributeByParameter = 22,
        RoomByTemp0 = 23,
        NeighborInStackObject = 24,
        Local = 25,
        Tuning = 26,
        DynSpriteFlagForTempOfStackObject = 27,
        TreeAdPersonalityVar = 28,
        TreeAdMin = 29,
        MyPersonDataByTemp = 30,
        StackObjectPersonDataByTemp = 31,
        NeighborPersonData = 32,
        JobData = 33,
        NeighborhoodData = 34,
        StackObjectFunction = 35, //see wiki
        MyTypeAttr = 36,
        StackObjectTypeAttr = 37,
        NeighborsObjectDefinition = 38, //really
        Unused = 39,
        LocalByTemp = 40,
        StackObjectAttributeByTemp = 41,
        TempXL = 42,
        CityTime = 43,
        TSOStandardTime = 44,
        GameTime = 45,
        MyList = 46,
        StackObjectList = 47,
        MoneyOverHead32Bit = 48,
        MyLeadTileAttribute = 49,
        StackObjectLeadTileAttribute = 50,
        MyLeadTile = 51,
        StackObjectLeadTile = 52,
        StackObjectMasterDef = 53,
        FeatureEnableLevel = 54,
        //if we ever get exceptions from accesses to 55-58 we should check over them...
        MyAvatarID = 59,

        INVALID = 255
    }
}
