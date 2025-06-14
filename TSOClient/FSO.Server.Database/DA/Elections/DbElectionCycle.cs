
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
namespace FSO.Server.Database.DA.Elections
{
    public class DbElectionCycle
    {
        public uint cycle_id { get; set; }
        public uint start_date { get; set; }
        public uint end_date { get; set; }
        public DbElectionCycleState current_state { get; set; }
        public DbElectionCycleType election_type { get; set; }

        //for free vote
        public string name { get; set; }
        public int nhood_id { get; set; }
    }

    public enum DbElectionCycleState : byte
    {
        shutdown,
        nomination,
        election,
        ended,
        failsafe
    }

    public enum DbElectionCycleType : byte
    {
        election,
        shutdown
    }
}
