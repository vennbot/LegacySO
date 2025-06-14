
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
using FSO.Server.Clients.Framework;
using FSO.Server.Protocol.CitySelector;
using RestSharp;
using System;
using System.Collections.Generic;

namespace FSO.Server.Clients
{
    public class CityClient : AbstractHttpClient
    {
        public CityClient(string baseUrl) : base(baseUrl) {
        }

        public ShardSelectorServletResponse ShardSelectorServlet(ShardSelectorServletRequest input)
        {
            var client = Client();

            var request = new RestRequest("cityselector/app/ShardSelectorServlet")
                            .AddQueryParameter("shardName", input.ShardName)
                            .AddQueryParameter("avatarId", input.AvatarID);
            
            var response = client.Execute(request);
            if(response.StatusCode != System.Net.HttpStatusCode.OK){
                throw new Exception("Unknown error during ShardSelectorServlet");
            }

            return XMLUtils.Parse<ShardSelectorServletResponse>(response.Content);
        }


        public InitialConnectServletResult InitialConnectServlet(InitialConnectServletRequest input)
        {
            var client = Client();

            var request = new RestRequest("cityselector/app/InitialConnectServlet")
                            .AddQueryParameter("ticket", input.Ticket)
                            .AddQueryParameter("version", input.Version);

            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Unknown error during InitialConnectServlet");
            }

            return XMLUtils.Parse<InitialConnectServletResult>(response.Content);
        }

        public List<AvatarData> AvatarDataServlet()
        {
            var client = Client();

            var request = new RestRequest("cityselector/app/AvatarDataServlet");

            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Unknown error during AvatarDataServlet");
            }

            var aotDummy = new XMLList<AvatarData>();
            List<AvatarData> result = (List<AvatarData>)XMLUtils.Parse<XMLList<AvatarData>>(response.Content);
            return result;
        }


        public List<ShardStatusItem> ShardStatus()
        {
            var client = Client();

            var request = new RestRequest("cityselector/shard-status.jsp");

            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Unknown error during ShardStatus");
            }

            var aotDummy = new XMLList<ShardStatusItem>();
            List<ShardStatusItem> result = (List<ShardStatusItem>)XMLUtils.Parse<XMLList<ShardStatusItem>>(response.Content);
            return result;
        }
    }
}
