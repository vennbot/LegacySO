
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
using FSO.Server.Database.DA.Hosts;
using FSO.Server.Framework.Gluon;
using FSO.Server.Protocol.Gluon.Packets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSO.Server.Domain
{
    public interface IGluonHostPool
    {
        string PoolHash { get; }

        IGluonHost Get(string callSign);
        IGluonHost GetByShardId(int shard_id);
        IEnumerable<IGluonHost> GetByRole(DbHostRole role);
        IEnumerable<IGluonHost> GetAll();

        void Start();
        void Stop();
    }

    public interface IGluonHost : IGluonSession
    {
        DbHostRole Role { get; }
        bool Connected { get; }
        DateTime BootTime { get; }
        Task<IGluonCall> Call<IN>(IN input) where IN : IGluonCall;
    }
}
