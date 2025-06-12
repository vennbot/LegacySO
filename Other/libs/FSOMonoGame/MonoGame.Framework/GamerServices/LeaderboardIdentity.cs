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
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework.GamerServices
{
    [DataContract]
    public struct LeaderboardIdentity
    {
        [DataMember]
        public int GameMode { get; set; }

        [DataMember]
        public LeaderboardKey Key { get; set; }

        public static LeaderboardIdentity Create(LeaderboardKey aKey)
        {
            return new LeaderboardIdentity() { Key = aKey};
        }

        public static LeaderboardIdentity Create(LeaderboardKey aKey, int aGameMode)
        {
            return new LeaderboardIdentity() { Key = aKey, GameMode = aGameMode};
        }
    }
}

