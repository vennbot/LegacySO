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

namespace FSO.Server.Database.DA.Outfits
{
    public class DbOutfit
    {
        public uint outfit_id { get; set; }
        public Nullable<uint> avatar_owner { get; set; }
        public Nullable<uint> object_owner { get; set; }
        public ulong asset_id { get; set; }
        public int sale_price { get; set; }
        public int purchase_price { get; set; }
        public byte outfit_type { get; set; }
        public DbOutfitSource outfit_source { get; set; }
    }

    public enum DbOutfitSource
    {
        cas,
        rack
    }
}
