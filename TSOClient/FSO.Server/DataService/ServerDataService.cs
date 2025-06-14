
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
using FSO.Common.Serialization;
using FSO.Server.DataService.Providers;
using Ninject;

namespace FSO.Server.DataService
{
    public class ServerDataService : FSO.Common.DataService.DataService
    {
        public ServerDataService(IModelSerializer serializer, 
                                FSO.Content.Content content,
                                IKernel kernel) : base(serializer, content)
        {
            AddProvider(kernel.Get<ServerAvatarProvider>());
            var lots = kernel.Get<ServerLotProvider>();
            AddProvider(lots);
            var city = kernel.Get<ServerCityProvider>();
            AddProvider(city);
            var nhood = kernel.Get<ServerNeighborhoodProvider>();
            nhood.BindCityRep(lots);
            AddProvider(nhood);

            var ratings = kernel.Get<ServerMayorRatingProvider>();
            AddProvider(ratings);

            city.BindLots(lots);
        }
    }
}
