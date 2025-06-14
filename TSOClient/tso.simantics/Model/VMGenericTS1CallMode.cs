
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
namespace FSO.SimAntics.Model
{
    public enum VMGenericTS1CallMode
    {
        HouseTutorialComplete = 0,
        SwapMyAndStackObjectsSlots = 1,
        SetActionIconToStackObject = 2,
        PullDownTaxiDialog = 3,
        AddToFamily = 4,
        CombineAssetsOfFamilyInTemp0 = 5,
        RemoveFromFamily = 6,
        MakeNewNeighbor = 7, //this one is "depracated"
        FamilyTutorialComplete = 8,
        ArchitectureTutorialComplete = 9,
        DisableBuildBuy = 10,
        EnableBuildBuy = 11,
        GetDistanceToCameraInTemp0 = 12,
        AbortInteractions = 13, //abort all interactions associated with the stack object
        HouseRadioStationEqualsTemp0 = 14,
        MyRoutingFootprintEqualsTemp0 = 15,
        ChangeNormalOutfit = 16, //changes the normal outfit of the sim to the next available suit
        ChangeToLotInTemp0 = 17,
        BuildTheDowntownSimAndPlaceObjIDInTemp0 = 18, 
        SpawnDowntownDateOfPersonInTemp0 = 19, //temp0 is replaced with autofollow sim in temp0
        SpawnTakeBackHomeDataOfPersonInTemp0 = 20, //same side effect as above
        SpawnInventorySimDataEffects = 21,
        SelectDowntownLot = 22, //displays the selection screen and returns a lot in temp0 (simulation paused)
        GetDowntownTimeFromSOInventory = 23, //hours in temp0, minutes in temp1
        HotDateChangeSuitsPermanentlyCall = 24,
        SaveSimPersistentData = 25, //motives, relationships...
        BuildVacationFamilyPutFamilyNumInTemp0 = 26,
        ReturnNumberOfAvaiableVacationLotsInTemp0 = 27,
        ReturnZoningTypeOfLotInTemp0 = 28,
        SetStackObjectsSuit = 29, //suit type in temp0, suit index in temp1. Returns old index in temp1.
        GetStackObjectsSuit = 30, //suit type in temp0, suit index in temp1.
        CountStackObjectSuits = 31, //suit type in temp0, suit count returned in temp1.
        CreatePurchasedPetsNearOwner = 32, 
        AddToFamilyInTemp0 = 33,
        PromoteFameIfNeeded = 34,
        TakeTaxiHook = 35,
        DemoteFameIfNeeded = 36,
        CancelPieMenu = 37,
        GetTokensFromString = 38,
        ChildToAdult = 39,
        PetToAdult = 40,
        HeadFlush = 41,
        MakeTemp0SelectedSim = 42,
        FamilySpellsIntoController = 43
    }
}
