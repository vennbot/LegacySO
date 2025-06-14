
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
using FSO.Server.Database.DA;
using FSO.Server.Database.DA.Tasks;
using FSO.Server.Domain;
using FSO.Server.Protocol.Gluon.Packets;

namespace FSO.Server.Servers.Tasks.Domain
{
    public class NeighborhoodsTask : ITask
    {
        private IDAFactory DAFactory;
        private IGluonHostPool HostPool;

        public NeighborhoodsTask(IDAFactory DAFactory, IGluonHostPool hostPool)
        {
            this.DAFactory = DAFactory;
            this.HostPool = hostPool;
        }

        public void Run(TaskContext context)
        {
            var cityServers = HostPool.GetByRole(Database.DA.Hosts.DbHostRole.city);

            foreach (var city in cityServers)
            {
                city.Write(new CityNotify(CityNotifyType.NhoodUpdate));
            }
        }

        public void Abort()
        {
        }

        public DbTaskType GetTaskType()
        {
            return DbTaskType.neighborhood_tick;
        }
    }
}
