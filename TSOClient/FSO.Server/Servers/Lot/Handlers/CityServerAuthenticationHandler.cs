
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
using FSO.Server.Framework.Gluon;
using FSO.Server.Protocol.Aries.Packets;
using FSO.Server.Protocol.Utils;
using FSO.Server.Servers.Lot.Lifecycle;

namespace FSO.Server.Servers.Lot.Handlers
{
    /// <summary>
    /// Establishes authentication with the city server
    /// </summary>
    public class CityServerAuthenticationHandler
    {
        private string Secret;

        public CityServerAuthenticationHandler(ServerConfiguration config)
        {
            this.Secret = config.Secret;
        }

        public void Handle(IGluonSession session, RequestClientSession request)
        {
            //Respond asking for a gluon challenge
            session.Write(new RequestChallenge() { CallSign = session.CallSign, PublicHost = session.PublicHost, InternalHost = session.InternalHost });
        }

        public void Handle(IGluonSession session, RequestChallengeResponse challenge)
        {
            var rawSession = ((CityConnection)session);
            var answer = ChallengeResponse.AnswerChallenge(challenge.Challenge, Secret);

            session.Write(new AnswerChallenge {
                Answer = answer
            });
        }

        public void Handle(IGluonSession session, AnswerAccepted accepted)
        {
            var rawSession = ((CityConnection)session);
            rawSession.AuthenticationEstablished();
        }
    }
}
