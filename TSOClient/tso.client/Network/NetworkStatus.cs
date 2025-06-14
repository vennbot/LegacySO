
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
using FSO.Files.RC;

namespace FSO.Client.Network
{
    public class NetworkStatus
    {
        public int CityReconnectAttempt;
        public int LotReconnectAttempt;
        public int RemeshesInProgress
        {
            get
            {
                return DGRP3DMesh.GetWorkCount();
            }
        }

        public bool Any
        {
            get
            {
                return CityReconnectAttempt > 0 || LotReconnectAttempt > 0 || RemeshesInProgress > 0;
            }
        }

        public bool Severe
        {
            get
            {
                return CityReconnectAttempt > 0 || LotReconnectAttempt > 0;
            }
        }
    }
}
