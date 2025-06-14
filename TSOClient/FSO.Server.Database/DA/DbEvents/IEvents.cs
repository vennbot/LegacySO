
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
using FSO.Server.Database.DA.Utils;
using System;
using System.Collections.Generic;

namespace FSO.Server.Database.DA.DbEvents
{
    public interface IEvents
    {
        PagedList<DbEvent> All(int offset = 0, int limit = 20, string orderBy = "start_day");
        List<DbEvent> GetActive(DateTime time);
        int Add(DbEvent evt);
        bool Delete(int event_id);

        bool TryParticipate(DbEventParticipation p);
        bool Participated(DbEventParticipation p);
        List<uint> GetParticipatingUsers(int event_id);

        bool GenericAvaTryParticipate(DbGenericAvatarParticipation p);
        bool GenericAvaParticipated(DbGenericAvatarParticipation p);
        List<uint> GetGenericParticipatingAvatars(string genericName);

        List<DbEvent> GetLatestNameDesc(int limit);
    }
}
