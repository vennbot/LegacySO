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

namespace FSO.Server.Database.DA.GlobalCooldowns
{
    public interface IGlobalCooldowns
    {
        DbGlobalCooldowns Get(uint objguid, uint avatarid, bool useAccount, uint category);
        List<DbGlobalCooldowns> GetAllByObj(uint objguid);
        List<DbGlobalCooldowns> GetAllByAvatar(uint avatarid);
        List<DbGlobalCooldowns> GetAllByObjectAndAvatar(uint objguid, uint avatarid);
        bool Create(DbGlobalCooldowns cooldown);
        bool Update(DbGlobalCooldowns cooldown);
    }
}
