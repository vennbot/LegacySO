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
using FSO.Common.Enum;

namespace FSO.Server.Database.DA.Bonus
{
    public class DbBonusMetrics
    {
        public uint avatar_id { get; set; }
        public int lot_id { get; set; }
        public LotCategory category { get; set; }
        public int? visitor_minutes { get; set; }
        public byte? property_rank { get; set; }
        public byte? sim_rank { get; set; }
    }
}
