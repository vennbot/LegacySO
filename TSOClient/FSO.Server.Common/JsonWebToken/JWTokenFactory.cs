
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
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FSO.Server.Servers.Api.JsonWebToken
{
    public class JWTInstance
    {
        public string Token;
        public int ExpiresIn;
    }

    public class JWTFactory
    {
        private JWTConfiguration Config;

        public JWTFactory(JWTConfiguration config)
        {
            this.Config = config;
        }

        public JWTUser DecodeToken(string token)
        {
            var payload = JWT.JsonWebToken.Decode(token, Config.Key, true);
            Dictionary<string, string> payloadParsed = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<JWTUser>(payloadParsed["data"]);
        }

        public JWTInstance CreateToken(JWTUser data)
        {
            var tokenData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return CreateToken(tokenData, Config.TokenDuration);
        }

        private JWTInstance CreateToken(string data, int expiresIn)
        {
            var expires = Epoch.Now + expiresIn;
            var payload = new Dictionary<string, object>()
            {
                { "exp", expires },
                { "data", data }
            };

            var token = JWT.JsonWebToken.Encode(payload, Config.Key, JWT.JwtHashAlgorithm.HS384);
            return new JWTInstance { Token = token, ExpiresIn = expiresIn };
        }
    }
}
