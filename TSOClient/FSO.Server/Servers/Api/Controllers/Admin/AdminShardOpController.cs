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
using FSO.Server.Protocol.Gluon.Model;
using FSO.Server.Servers.Api.JsonWebToken;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSO.Server.Servers.Api.Controllers.Admin
{
    public class AdminShardOpController : NancyModule
    {
        private IDAFactory DAFactory;
        private ApiServer Server;

        public AdminShardOpController(IDAFactory daFactory, JWTFactory jwt, ApiServer server) : base("/admin/shards")
        {
            JWTTokenAuthentication.Enable(this, jwt);
            
            this.DAFactory = daFactory;
            this.Server = server;

            this.After.AddItemToEndOfPipeline(x =>
            {
                x.Response.WithHeader("Access-Control-Allow-Origin", "*");
            });

            this.Post["/shutdown"] = _ =>
            {
                this.DemandAdmin();
                var shutdown = this.Bind<ShutdownModel>();

                ShutdownType type = ShutdownType.SHUTDOWN;
                if (shutdown.update) type = ShutdownType.UPDATE;
                else if (shutdown.restart) type = ShutdownType.RESTART;

                //JWTUserIdentity user = (JWTUserIdentity)this.Context.CurrentUser;
                Server.RequestShutdown((uint)shutdown.timeout, type);

                return Response.AsJson(true);
            };

            this.Post["/announce"] = _ =>
            {
                this.DemandModerator();
                var announce = this.Bind<AnnouncementModel>();

                Server.BroadcastMessage(announce.sender, announce.subject, announce.message);

                return Response.AsJson(true);
            };
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
