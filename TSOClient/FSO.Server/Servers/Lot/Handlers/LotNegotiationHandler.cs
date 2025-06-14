
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
using FSO.Server.Protocol.Gluon.Packets;
using FSO.Server.Servers.Lot.Domain;
using NLog;

namespace FSO.Server.Servers.Lot.Handlers
{
    public class LotNegotiationHandler
    {
        private static Logger LOG = LogManager.GetCurrentClassLogger();
        private LotHost Lots;

        public LotNegotiationHandler(LotHost lots)
        {
            this.Lots = lots;
        }

        public void Handle(IGluonSession session, TransferClaim request)
        {
            LOG.Info("Recieved lot host request... ");

            if (request.Type != Protocol.Gluon.Model.ClaimType.LOT)
            {
                session.Write(new TransferClaimResponse {
                    Status = TransferClaimResponseStatus.REJECTED,
                    Type = request.Type,
                    ClaimId = request.ClaimId,
                    EntityId = request.EntityId
                });
                return;
            }

            var lot = Lots.TryHost(request.EntityId, session);
            if(lot == null)
            {
                session.Write(new TransferClaimResponse
                {
                    Status = TransferClaimResponseStatus.REJECTED,
                    Type = request.Type,
                    ClaimId = request.ClaimId,
                    EntityId = request.EntityId
                });
                return;
            }

            if(Lots.TryAcceptClaim((int)request.EntityId, request.ClaimId, request.SpecialId, request.FromOwner, request.Action))
            {
                session.Write(new TransferClaimResponse
                {
                    Status = TransferClaimResponseStatus.ACCEPTED,
                    Type = request.Type,
                    ClaimId = request.ClaimId,
                    EntityId = request.EntityId
                });
            }
            else
            {
                session.Write(new TransferClaimResponse
                {
                    Status = TransferClaimResponseStatus.CLAIM_NOT_FOUND,
                    Type = request.Type,
                    ClaimId = request.ClaimId,
                    EntityId = request.EntityId
                });
            }
        }

        public void Handle(IGluonSession session, RequestLotClientTermination request)
        {
            Lots.TryDisconnectClient(request.LotId, request.AvatarId);
        }

        public void Handle(IGluonSession session, NotifyLotRoommateChange request)
        {
            Lots.NotifyRoommateChange(request.LotId, request.AvatarId, request.ReplaceId, request.Change);
        }

        public void Handle(IGluonSession session, TuningChanged request)
        {
            Lots.UpdateTuning(request.UpdateInstantly);
        }
    }
}
