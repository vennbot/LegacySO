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
using FSO.Server.Database.DA;
using FSO.Server.Database.DA.Tasks;
using FSO.Server.Domain;
using FSO.Server.Protocol.Gluon.Packets;
using FSO.Server.Servers.Api.JsonWebToken;
using FSO.Server.Utils;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSO.Server.Servers.Api.Controllers.Admin
{
    public class AdminTasksController : NancyModule
    {
        public AdminTasksController(IDAFactory daFactory, JWTFactory jwt, IGluonHostPool hostPool) : base("/admin")
        {
            JWTTokenAuthentication.Enable(this, jwt);

            this.Get["/tasks"] = _ =>
            {
                this.DemandAdmin();

                using (var da = daFactory.Get())
                {
                    var offset = this.Request.Query["offset"];
                    var limit = this.Request.Query["limit"];

                    if (offset == null) { offset = 0; }
                    if (limit == null) { limit = 20; }

                    if (limit > 100)
                    {
                        limit = 100;
                    }

                    var result = da.Tasks.All((int)offset, (int)limit);
                    return Response.AsPagedList<DbTask>(result);
                }
            };

            this.Post["/tasks/request"] = x =>
            {
                var task = this.Bind<TaskRequest>();

                var taskServer = hostPool.GetByRole(Database.DA.Hosts.DbHostRole.task).FirstOrDefault();
                if(taskServer == null)
                {
                    return Response.AsJson(-1);
                }else{
                    try {
                        var id = taskServer.Call(new RequestTask() {
                            TaskType = task.task_type.ToString(),
                            ParameterJson = JsonConvert.SerializeObject(task.parameter),
                            ShardId = (task.shard_id == null || !task.shard_id.HasValue) ? -1 : task.shard_id.Value
                        }).Result;
                        return Response.AsJson(id);
                    }catch(Exception ex)
                    {
                        return Response.AsJson(-1);
                    }
                }
            };
        }
    }

    public class TaskRequest
    {
        public DbTaskType task_type;
        public int? shard_id;
        public dynamic parameter;
    }
}
