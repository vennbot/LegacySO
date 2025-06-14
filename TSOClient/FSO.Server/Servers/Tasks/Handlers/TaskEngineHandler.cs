
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
using FSO.Server.Framework.Gluon;
using FSO.Server.Protocol.Gluon.Packets;
using Newtonsoft.Json;
using System;

namespace FSO.Server.Servers.Tasks.Handlers
{
    public class TaskEngineHandler
    {
        private TaskEngine TaskEngine;

        public TaskEngineHandler(TaskEngine engine)
        {
            this.TaskEngine = engine;
        }

        public void Handle(IGluonSession session, RequestTask task)
        {
            var shardId = new Nullable<int>();
            if(task.ShardId > 0){
                shardId = task.ShardId;
            }

            var id = TaskEngine.Run(new TaskRunOptions() {
                Task = task.TaskType,
                Shard_Id = shardId,
                Parameter = JsonConvert.DeserializeObject(task.ParameterJson)
            });
            session.Write(new RequestTaskResponse() {
                CallId = task.CallId,
                TaskId = id
            });
        }
    }
}
