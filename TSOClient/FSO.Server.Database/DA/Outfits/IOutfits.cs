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

namespace FSO.Server.Database.DA.Outfits
{
    public interface IOutfits
    {
        uint Create(DbOutfit outfit);
        List<DbOutfit> GetByObjectId(uint object_id);
        List<DbOutfit> GetByAvatarId(uint avatar_id);

        bool UpdatePrice(uint outfit_id, uint object_id, int new_price);
        bool ChangeOwner(uint outfit_id, uint object_owner, uint new_avatar_owner);
        bool DeleteFromObject(uint outfit_id, uint object_id);
        bool DeleteFromAvatar(uint outfit_id, uint avatar_id);
    }
}
