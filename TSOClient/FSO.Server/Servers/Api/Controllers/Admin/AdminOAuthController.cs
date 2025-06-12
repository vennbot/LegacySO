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
using FSO.Server.Common;
using FSO.Server.Database.DA;
using FSO.Server.Servers.Api.JsonWebToken;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSO.Server.Servers.Api.Controllers.Admin
{
    public class AdminOAuthController : NancyModule
    {
        public AdminOAuthController(IDAFactory daFactory, JWTFactory jwt) : base("/admin/oauth")
        {
            this.Post["/token"] = _ =>
            {
                var grant_type = this.Request.Form["grant_type"];

                if (grant_type == "password")
                {
                    var username = this.Request.Form["username"];
                    var password = this.Request.Form["password"];

                    using (var da = daFactory.Get())
                    {
                        var user = da.Users.GetByUsername(username);
                        if (user == null || user.is_banned || !(user.is_admin || user.is_moderator))
                        {
                            return Response.AsJson<OAuthError>(new OAuthError
                            {
                                error = "unauthorized_client",
                                error_description = "user_credentials_invalid"
                            });
                        }

                        var authSettings = da.Users.GetAuthenticationSettings(user.user_id);
                        var isPasswordCorrect = PasswordHasher.Verify(password, new PasswordHash
                        {
                            data = authSettings.data,
                            scheme = authSettings.scheme_class
                        });

                        if (!isPasswordCorrect)
                        {
                            return Response.AsJson<OAuthError>(new OAuthError
                            {
                                error = "unauthorized_client",
                                error_description = "user_credentials_invalid"
                            });
                        }

                        JWTUserIdentity identity = new JWTUserIdentity();
                        identity.UserName = user.username;
                        var claims = new List<string>();
                        if (user.is_admin || user.is_moderator)
                        {
                            claims.Add("moderator");
                        }
                        if (user.is_admin)
                        {
                            claims.Add("admin");
                        }

                        identity.Claims = claims;
                        identity.UserID = user.user_id;

                        var token = jwt.CreateToken(identity);
                        return Response.AsJson<OAuthSuccess>(new OAuthSuccess
                        {
                            access_token = token.Token,
                            expires_in = token.ExpiresIn
                        });
                    }
                }

                return Response.AsJson<OAuthError>(new OAuthError
                {
                    error = "invalid_request",
                    error_description = "unknown grant_type"
                });
            };
        }
    }


    public class OAuthError
    {
        public string error_description { get; set; }
        public string error { get; set; }
    }

    public class OAuthSuccess
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}
