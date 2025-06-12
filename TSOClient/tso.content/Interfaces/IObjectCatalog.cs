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
using System.Collections.Generic;

namespace FSO.Content.Interfaces
{
    public interface IObjectCatalog
    {
        List<ObjectCatalogItem> All();
        List<ObjectCatalogItem> GetItemsByCategory(sbyte category);
        ObjectCatalogItem? GetItemByGUID(uint guid);
        List<uint> GetUntradableGUIDs();
    }

    public struct ObjectCatalogItem
    {
        public uint GUID;
        public sbyte Category;
        public uint Price;
        public string Name;
        public string CatalogName;
        public string Tags;
        public byte DisableLevel; //1 = only shopping, 2 = rare (unsellable?)

        public byte RoomSort;
        public byte Subsort;
        public byte DowntownSort;
        public byte VacationSort;
        public byte CommunitySort;
        public byte StudiotownSort;
        public byte MagictownSort;
    }
}
