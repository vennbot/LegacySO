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
using FSO.Server.Api.Core.Utils;
using FSO.Server.Common;
using FSO.Server.Servers.Api.JsonWebToken;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FSO.Server.Api.Core.Controllers.Admin
{
    [EnableCors("AdminAppPolicy")]
    [Route("admin/oauth/token")]
    [ApiController]
    public class AdminOAuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromForm] AuthRequest auth)
        {
            if (auth == null) Ok();
            if (auth.grant_type == "password")
            {
                var api = Api.INSTANCE;
                using (var da = api.DAFactory.Get())
                {
                    var user = da.Users.GetByUsername(auth.username);
                    if (user == null || user.is_banned || !(user.is_admin || user.is_moderator))
                    {
                        return ApiResponse.Json(System.Net.HttpStatusCode.OK, new OAuthError
                        {
                            error = "unauthorized_client",
                            error_description = "user_credentials_invalid"
                        });
                    }

                    var ip = ApiUtils.GetIP(Request);
                    var accLock = da.Users.GetRemainingAuth(user.user_id, ip);
                    if (accLock != null && (accLock.active || accLock.count >= AuthLoginController.LockAttempts) && accLock.expire_time > Epoch.Now)
                    {
                        return ApiResponse.Json(System.Net.HttpStatusCode.OK, new OAuthError
                        {
                            error = "unauthorized_client",
                            error_description = "account_locked"
                        });
                    }

                    var authSettings = da.Users.GetAuthenticationSettings(user.user_id);
                    var isPasswordCorrect = PasswordHasher.Verify(auth.password, new PasswordHash
                    {
                        data = authSettings.data,
                        scheme = authSettings.scheme_class
                    });

                    if (!isPasswordCorrect)
                    {
                        var durations = AuthLoginController.LockDuration;
                        var failDelay = 60 * durations[Math.Min(durations.Length - 1, da.Users.FailedConsecutive(user.user_id, ip))];
                        if (accLock == null)
                        {
                            da.Users.NewFailedAuth(user.user_id, ip, (uint)failDelay);
                        }
                        else
                        {
                            var remaining = da.Users.FailedAuth(accLock.attempt_id, (uint)failDelay, AuthLoginController.LockAttempts);
                        }

                        return ApiResponse.Json(System.Net.HttpStatusCode.OK, new OAuthError
                        {
                            error = "unauthorized_client",
                            error_description = "user_credentials_invalid"
                        });
                    }

                    da.Users.SuccessfulAuth(user.user_id, ip);

                    JWTUser identity = new JWTUser();
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

                    var token = api.JWT.CreateToken(identity);

                    var response = ApiResponse.Json(System.Net.HttpStatusCode.OK, new OAuthSuccess
                    {
                        access_token = token.Token,
                        expires_in = token.ExpiresIn
                    });

                    return response;
                }
            }

            return ApiResponse.Json(System.Net.HttpStatusCode.OK, new OAuthError
            {
                error = "invalid_request",
                error_description = "unknown grant_type"
            });
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

    public class AuthRequest
    {
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
