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
using FSO.Common.DataService.Framework;
using FSO.Server.Common;
using FSO.Server.Database.DA;
using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSO.Server.Servers.Api.Controllers.UserAPI
{
    public class CityInfoController : NancyModule
    {
        private IDAFactory DAFactory;
        private IServerNFSProvider NFS;
        private static object ModelLock = new object { };
        private static CityInfoModel LastModel = new CityInfoModel();
        private static uint LastModelUpdate;

        /*
         * TODO: city data service access for desired shards. 
         * Need to maintain connections to shards and request from their data services... 
         * Either that or online status has to at least writeback to DB.
         */

        public CityInfoController(IDAFactory daFactory, IServerNFSProvider nfs) : base("/userapi/city")
        {
            this.DAFactory = daFactory;
            this.NFS = nfs;

            this.After.AddItemToEndOfPipeline(x =>
            {
                x.Response.WithHeader("Access-Control-Allow-Origin", "*");
            });

            this.Get["/{shardid}/{id}.png"] = parameters =>
            {
                using (var da = daFactory.Get())
                {
                    var lot = da.Lots.GetByLocation((int)parameters.shardid, (uint)parameters.id);
                    if (lot == null) return HttpStatusCode.NotFound;
                    return Response.AsImage(Path.Combine(NFS.GetBaseDirectory(), "Lots/" + lot.lot_id.ToString("x8") + "/thumb.png"));
                }
            };

            this.Get["/{shardid}/city.json"] = parameters =>
            {
                var now = Epoch.Now;
                if (LastModelUpdate < now - 15) {
                    LastModelUpdate = now;
                    lock (ModelLock)
                    {
                        LastModel = new CityInfoModel();
                        using (var da = daFactory.Get())
                        {
                            var lots = da.Lots.AllLocations((int)parameters.shardid);
                            var lotstatus = da.LotClaims.AllLocations((int)parameters.shardid);
                            LastModel.reservedLots = lots.ConvertAll(x => x.location).ToArray();
                            LastModel.names = lots.ConvertAll(x => x.name).ToArray();
                            LastModel.activeLots = lotstatus.ConvertAll(x => x.location).ToArray();
                            LastModel.onlineCount = lotstatus.ConvertAll(x => x.active).ToArray();
                        }
                    }
                }
                lock (ModelLock)
                {
                    return Response.AsJson(LastModel);
                }
            };
        }
    }

    class CityInfoModel
    {
        public string[] names;
        public uint[] reservedLots;
        public uint[] activeLots;
        public int[] onlineCount;
    }
}
