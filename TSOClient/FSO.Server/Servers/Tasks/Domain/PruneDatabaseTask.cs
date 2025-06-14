
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
using FSO.Server.Database.DA.Tasks;
using FSO.Server.Database.DA;
using FSO.Server.Common;

namespace FSO.Server.Servers.Tasks.Domain
{
    public class PruneDatabaseTask : ITask
    {
        private IDAFactory DAFactory;

        public PruneDatabaseTask(IDAFactory DAFactory)
        {
            this.DAFactory = DAFactory;
        }

        public void Run(TaskContext context)
        {
            using (var db = DAFactory.Get())
            {
                //Purge old shard & auth tickets
                var expireTime = Epoch.Now - 600;
                db.AuthTickets.Purge(expireTime);
                db.Shards.PurgeTickets(expireTime);

                var retentionPeriod = Epoch.Now - (86400 * 14);
                var retentionDate = Epoch.ToDate(retentionPeriod);

                db.LotVisitTotals.Purge(retentionDate);
                db.Bonus.Purge(retentionDate);
                db.LotVisits.PurgeByDate(retentionDate);

                var days = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalDays;
                db.Transactions.Purge((int)days - 30);
            }
        }

        public void Abort()
        {
        }

        public DbTaskType GetTaskType()
        {
            return DbTaskType.prune_database;
        }
    }
}
