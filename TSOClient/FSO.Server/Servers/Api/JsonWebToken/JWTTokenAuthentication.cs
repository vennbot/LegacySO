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
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSO.Server.Servers.Api.JsonWebToken
{
    public class JWTTokenAuthentication
    {
        private const string Scheme = "bearer";

        public static void Enable(INancyModule module, JWTFactory factory)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }

            module.Before.AddItemToStartOfPipeline(GetCredentialRetrievalHook(factory));
        }

        private static Func<NancyContext, Response> GetCredentialRetrievalHook(JWTFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("configuration");
            }

            return context =>
            {
                RetrieveCredentials(context, factory);
                return null;
            };
        }

        private static void RetrieveCredentials(NancyContext context, JWTFactory factory)
        {
            var token = ExtractTokenFromHeader(context.Request);
            if (token == null)
            {
                return;
            }

            try {
                var user = factory.DecodeToken(token);
                if (user != null) {
                    var identity = new JWTUserIdentity()
                    {
                        UserID = user.UserID,
                        UserName = user.UserName,
                        Claims = user.Claims
                    };
                    context.CurrentUser = identity;
                }
            }catch(Exception ex){
                //Expired
            }
        }

        private static string ExtractTokenFromHeader(Request request)
        {
            var authorization = request.Headers.Authorization;

            if (string.IsNullOrEmpty(authorization))
            {
                //City selector puts it in a cookie
                if (request.Cookies.ContainsKey("fso"))
                {
                    return request.Cookies["fso"];
                }
                return null;
            }

            if (!authorization.StartsWith(Scheme))
            {
                return null;
            }

            try
            {
                var encodedToken = authorization.Substring(Scheme.Length).Trim();
                return String.IsNullOrWhiteSpace(encodedToken) ? null : encodedToken;
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}
