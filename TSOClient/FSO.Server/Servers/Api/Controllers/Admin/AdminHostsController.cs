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
using FSO.Server.Domain;
using FSO.Server.Servers.Api.JsonWebToken;
using FSO.Server.Utils;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSO.Server.Servers.Api.Controllers.Admin
{
    public class AdminHostsController : NancyModule
    {
        public AdminHostsController(IDAFactory daFactory, JWTFactory jwt, IGluonHostPool hostPool) : base("/admin")
        {
            JWTTokenAuthentication.Enable(this, jwt);

            this.Get["/hosts"] = _ =>
            {
                this.DemandAdmin();
                var hosts = hostPool.GetAll();

                return Response.AsJson(hosts.Select(x => new {
                    role = x.Role,
                    call_sign = x.CallSign,
                    internal_host = x.InternalHost,
                    public_host = x.PublicHost,
                    connected = x.Connected,
                    time_boot = x.BootTime
                }));
            };
        }
    }
}
