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
using FSO.Server.Api.Core.Utils;
using FSO.Server.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FSO.Server.Api.Core.Controllers.Admin
{
    [EnableCors("AdminAppPolicy")]
    [Route("admin/shards")]
    [ApiController]
    public class AdminShardsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var api = Api.INSTANCE;
            api.DemandAdmin(Request);

            using (var db = api.DAFactory.Get())
            {
                var shards = db.Shards.All();
                return ApiResponse.Json(HttpStatusCode.OK, shards);
            }
        }

        [HttpPost("shutdown")]
        public IActionResult shutdown(ShutdownModel sd)
        {
            var api = Api.INSTANCE;
            api.DemandAdmin(Request);

            ShutdownType type = ShutdownType.SHUTDOWN;
            if (sd.update) type = ShutdownType.UPDATE;
            else if (sd.restart) type = ShutdownType.RESTART;

            api.RequestShutdown((uint)sd.timeout, type);

            return ApiResponse.Json(HttpStatusCode.OK, true);
        }

        [HttpPost("announce")]
        public IActionResult announce(AnnouncementModel an)
        {
            var api = Api.INSTANCE;
            api.DemandModerator(Request);

            api.BroadcastMessage(an.sender, an.subject, an.message);

            return ApiResponse.Json(HttpStatusCode.OK, true);
        }
    }

    public class AnnouncementModel
    {
        public string sender;
        public string subject;
        public string message;
        public int[] shard_ids;
    }

    public class ShutdownModel
    {
        public int timeout;
        public bool restart;
        public bool update;
        public int[] shard_ids;
    }
}
