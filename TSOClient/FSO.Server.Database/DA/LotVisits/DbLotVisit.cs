
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
using FSO.Common.Enum;
using System;

namespace FSO.Server.Database.DA.LotVisitors
{
    public class DbLotVisit
    {
        public int lot_visit_id { get; set; }
        public uint avatar_id { get; set; }
        public int lot_id { get; set; }
        public DbLotVisitorType type { get; set; }
        public DbLotVisitorStatus status { get; set; }
        public DateTime time_created { get; set; }
        public DateTime? time_closed { get; set; }
    }

    public class DbLotVisitNhood : DbLotVisit
    {
        public uint neighborhood_id { get; set; }
        public uint location { get; set; }
        public LotCategory category { get; set; }
    }

    public enum DbLotVisitorType
    {
        owner,
        roommate,
        visitor
    }

    public enum DbLotVisitorStatus
    {
        active,
        closed,
        failed
    }
}
