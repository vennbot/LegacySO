
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
using FSO.Server.Database.DA;
using FSO.Server.Framework.Gluon;
using FSO.Server.Protocol.Gluon.Packets;
using FSO.Server.Servers.City.Domain;
using System;

namespace FSO.Server.Servers.City.Handlers
{
    public class LotServerClosedownHandler
    {
        private LotAllocations Lots;
        private IDAFactory DAFactory;

        public LotServerClosedownHandler(LotAllocations lots, IDAFactory daFactory)
        {
            this.Lots = lots;
            this.DAFactory = daFactory;
        }

        public void Handle(IGluonSession session, TransferClaim request)
        {
            if (request.Type != Protocol.Gluon.Model.ClaimType.LOT)
            {
                //what?
                session.Write(new TransferClaimResponse
                {
                    Status = TransferClaimResponseStatus.REJECTED,
                    Type = request.Type,
                    ClaimId = request.ClaimId,
                    EntityId = request.EntityId
                });
                return;
            }

            Lots.TryClose(request.EntityId, request.ClaimId);
            try
            {
                using (var db = DAFactory.Get())
                {
                    db.LotClaims.Delete(request.ClaimId, request.FromOwner);
                }
            }
            catch (Exception e)
            {
                //probably already unclaimed. do nothing.
            }
        }
    }
}
