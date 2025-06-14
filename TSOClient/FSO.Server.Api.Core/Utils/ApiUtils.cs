
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
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace FSO.Server.Api.Core.Utils
{
    public class ApiUtils
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage =
            "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        public static string GetIP(HttpRequest request)
        {
            var api = FSO.Server.Api.Core.Api.INSTANCE;
            if (!api.Config.UseProxy)
            {
                return request.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            else
            {
                var ip = "127.0.0.1";
                var xff = request.Headers["X-Forwarded-For"];
                if (xff.Count != 0)
                {
                    ip = xff.First();
                    ip = ip.Substring(ip.IndexOf(",") + 1);
                    var last = ip.LastIndexOf(":");
                    if (last != -1 && last < ip.Length - 5) ip = ip.Substring(0, last);
                }
                return ip;
            }
        }
    }
}
