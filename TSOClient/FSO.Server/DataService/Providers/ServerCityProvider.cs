
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
using FSO.Common.DataService.Framework;
using FSO.Common.DataService.Model;
using System.Threading.Tasks;

namespace FSO.Server.DataService.Providers
{
    public class ServerCityProvider : AbstractDataServiceProvider<uint, City>
    {
        private ServerLotProvider Lots;

        public void BindLots(ServerLotProvider lots)
        {
            Lots = lots;
        }

        public override Task<object> Get(object key)
        {
            //The lot provider actually knows a lot about this anyways, so they provide out sole city object.
            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(Lots.CityRepresentation);
            return tcs.Task;
        }
    }
}
