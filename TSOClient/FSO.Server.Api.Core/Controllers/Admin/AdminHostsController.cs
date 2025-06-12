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
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace FSO.Server.Api.Core.Controllers.Admin
{
    [EnableCors("AdminAppPolicy")]
    [Route("admin/hosts")]
    [ApiController]
    public class AdminHostsController : ControllerBase
    {
        public IActionResult Get()
        {
            var api = Api.INSTANCE;
            api.DemandAdmin(Request);
            var hosts = api.HostPool.GetAll();

            return ApiResponse.Json(HttpStatusCode.OK, hosts.Select(x => new {
                role = x.Role,
                call_sign = x.CallSign,
                internal_host = x.InternalHost,
                public_host = x.PublicHost,
                connected = x.Connected,
                time_boot = x.BootTime
            }));
        }
    }
}
