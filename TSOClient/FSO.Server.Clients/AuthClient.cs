
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
using FSO.Server.Clients.Framework;
using FSO.Server.Protocol.Authorization;
using RestSharp;
using System;

namespace FSO.Server.Clients
{
    public class AuthClient : AbstractHttpClient
    {
        public AuthClient(string baseUrl) : base(baseUrl) {
        }

        public AuthResult Authenticate(AuthRequest input)
        {
            var client = Client();

            var request = new RestRequest("AuthLogin")
                            .AddQueryParameter("username", input.Username)
                            .AddQueryParameter("password", input.Password)
                            .AddQueryParameter("serviceID", input.ServiceID)
                            .AddQueryParameter("version", input.Version)
                            .AddQueryParameter("clientid", input.ClientID);

            
            var response = client.Execute(request);
            var result = new AuthResult();
            result.Valid = false;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var lines = response.Content.Split(new char[] { '\n' });
                foreach (var line in lines)
                {
                    var components = line.Trim().Split(new char[] { '=' }, 2);
                    if (components.Length != 2) { continue; }

                    switch (components[0])
                    {
                        case "Valid":
                            result.Valid = Boolean.Parse(components[1]);
                            break;
                        case "Ticket":
                            result.Ticket = components[1];
                            break;
                        case "reasoncode":
                            result.ReasonCode = components[1];
                            break;
                        case "reasontext":
                            result.ReasonText = components[1];
                            break;
                        case "reasonurl":
                            result.ReasonURL = components[1];
                            break;
                    }
                }
            } else
            {
                result.ReasonCode = "36 301";
            }

            return result;
        }
    }
}
