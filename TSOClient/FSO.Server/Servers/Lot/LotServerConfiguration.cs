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
using FSO.Server.Framework.Aries;

namespace FSO.Server.Servers.Lot
{
    public class LotServerConfiguration : AbstractAriesServerConfig
    {
        public int Max_Lots = 1;

        public string SimNFS;
        public int RingBufferSize = 10;
        public bool Timeout_No_Auth = true;
        public bool LogJobLots = false;

        //Which cities to provide lot hosting for
        public LotServerConfigurationCity[] Cities;

        //How often to reconnect lost connections to city servers and report capacity
        public int CityReportingInterval = 10000;
    }

    public class LotServerConfigurationCity
    {
        public int ID;
        public string Host;
    }
}
