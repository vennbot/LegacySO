
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
using FSO.Server.Domain;
using FSO.Server.Framework.Aries;
using FSO.Server.Framework.Gluon;
using FSO.Server.Protocol.Aries.Packets;
using FSO.Server.Protocol.Gluon.Packets;
using FSO.Server.Protocol.Utils;

namespace FSO.Server.Servers.Shared.Handlers
{
    public class GluonAuthenticationHandler
    {
        private IGluonHostPool HostPool;
        private ISessions Sessions;
        private string Secret;

        public GluonAuthenticationHandler(ISessions sessions, ServerConfiguration config, IGluonHostPool hostPool){
            this.Sessions = sessions;
            this.Secret = config.Secret;
            this.HostPool = hostPool;
        }

        public void Handle(IAriesSession session, RequestChallenge request)
        {
            var challenge = ChallengeResponse.GetChallenge();
            session.SetAttribute("challenge", challenge);
            session.SetAttribute("callSign", request.CallSign);
            session.SetAttribute("publicHost", request.PublicHost);
            session.SetAttribute("internalHost", request.InternalHost);

            session.Write(new RequestChallengeResponse {
                Challenge = challenge
            });
        }

        public void Handle(AriesSession session, AnswerChallenge answer)
        {
            var challenge = session.GetAttribute("challenge") as string;
            if(challenge == null)
            {
                session.Close();
                return;
            }

            var myAnswer = ChallengeResponse.AnswerChallenge(challenge, Secret);
            if(myAnswer != answer.Answer)
            {
                session.Close();
                return;
            }

            //Trust established, good to go
            var newSession = Sessions.UpgradeSession<GluonSession>(session, x => {
                session.IsAuthenticated = true;
                x.Authenticate(Secret);
                x.CallSign = (string)session.GetAttribute("callSign");
                x.PublicHost = (string)session.GetAttribute("publicHost");
                x.InternalHost = (string)session.GetAttribute("internalHost");
            });
            newSession.Write(new AnswerAccepted());
        }

        public void Handle(IGluonSession session, HealthPing ping)
        {
            session.Write(new HealthPingResponse {
                CallId = ping.CallId,
                PoolHash = HostPool.PoolHash
            });
        }
    }
}
