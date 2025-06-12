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
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.GamerServices
{
    public sealed class LeaderboardReader : IDisposable
    {
        public LeaderboardReader ()
        {
        }

        /*
        public IAsyncResult BeginPageDown(AsyncCallback aAsyncCallback, object aAsyncState)
        {
            throw new NotImplementedException ();
        }

        public IAsyncResult BeginPageUp(AsyncCallback aAsyncCallback, object aAsyncState)
        {
            throw new NotImplementedException ();
        }

        public LeaderboardReader EndPageDown(IAsyncResult result)
        {
            throw new NotImplementedException ();
        }

        public LeaderboardReader EndPageUp(IAsyncResult result)
        {
            throw new NotImplementedException ();
        }

        public static void BeginRead (LeaderboardIdentity id, SignedInGamer gamer, int leaderboardPageSize, AsyncCallback leaderboardReadCallback, SignedInGamer gamer2)
        {
            throw new NotImplementedException ();
        }

        public static LeaderboardReader EndRead(IAsyncResult result)
        {
            throw new NotImplementedException ();
        }

        public void PageDown()
        {
            throw new NotImplementedException ();
        }

        public void PageUp()
        {
            throw new NotImplementedException ();
        }
        */

        public bool CanPageDown {
            get;
            set;
        }

        public bool CanPageUp {
            get;
            set;
        }

        public IEnumerable<LeaderboardEntry> Entries {
            get;
            set;
        }

        #region IDisposable implementation

        public void Dispose ()
        {
            throw new NotImplementedException ();
        }

        #endregion
    }
}

