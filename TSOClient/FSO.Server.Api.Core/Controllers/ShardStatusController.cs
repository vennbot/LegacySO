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
using FSO.Common.Utils;
using FSO.Server.Api.Core.Utils;
using FSO.Server.Protocol.CitySelector;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FSO.Server.Api.Core.Controllers
{
    [EnableCors]
    [Route("cityselector/shard-status.jsp")]
    [ApiController]
    public class ShardStatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var api = Api.INSTANCE;

            var result = new XMLList<ShardStatusItem>("Shard-Status-List");
            var shards = api.Shards.All;
            foreach (var shard in shards)
            {
                result.Add(shard);
            }
            return ApiResponse.Xml(HttpStatusCode.OK, result);
        }
    }
}
