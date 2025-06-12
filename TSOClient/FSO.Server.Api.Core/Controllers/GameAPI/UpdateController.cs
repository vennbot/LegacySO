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
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FSO.Server.Api.Core.Controllers.GameAPI
{
    [EnableCors]
    [Route("userapi/update")]
    public class UpdateController : ControllerBase
    {

        // GET userapi/update
        // get recent PUBLISHED updates for the active branch, ordered by publish date
        [HttpGet()]
        public IActionResult Get(int id)
        {
            var api = Api.INSTANCE;
            using (var da = api.DAFactory.Get())
            {
                var recents = da.Updates.GetRecentUpdatesForBranchByName(api.Config.BranchName, 20);
                return new JsonResult(recents.ToList());
            }
        }

        // GET: userapi/update/<branch>
        // get recent PUBLISHED updates for a specific branch, ordered by publish date
        [HttpGet("{branch}")]
        public IActionResult Get(string branch)
        {
            var api = Api.INSTANCE;
            using (var da = api.DAFactory.Get())
            {
                var recents = da.Updates.GetRecentUpdatesForBranchByName(branch, 20);
                return new JsonResult(recents.ToList());
            }
        }
    }
}
