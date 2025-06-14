
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
namespace FSO.Content.Model
{
    public class RackOutfit
    {
        public ulong AssetID { get; set; }
        public int Price { get; set; }
        public RackOutfitGender Gender { get; set; }
        public RackType RackType { get; set; }

        /*public ulong GetOutfitID()
        {
            return GetOutfitID(AssetID);
        }

        public static ulong GetOutfitID(ulong assetId)
        {
            return (assetId << 32) | 0xd;
        }*/
    }

    public enum RackOutfitGender
    {
        Male,
        Female,
        Neutral
    }

    public enum RackType : short
    {
        Daywear = 0,
        Formalwear = 1,
        Swimwear = 2,
        Sleepwear = 3,
        Decor_Head = 4,
        Decor_Back = 5,
        Decor_Shoe = 6,
        Decor_Tail = 7,
        CAS = 8
    }
}
